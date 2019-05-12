using UnityEngine;

public class ChildController : MonoBehaviour
{
    public static readonly float JumpStrength = 15f;
    public static readonly float SprintChancePerSecond = 0.2f;
    public static readonly float MinSpeed = 0.050f;
    public static readonly float MaxSpeed = 0.015f;

    private Transform _posCenter;
    private bool _isWaitingToJump = false;
    private float _jumpPos;
    private float _speed = 0f;

    private float _sprintDest;

    private Rigidbody2D _rb;
    private Animator animator;

    void Start()
    {
        _posCenter = GameObject.Find("ChildrenCenter").transform;
        _rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_isWaitingToJump && transform.position.x > _jumpPos)
        {
            _isWaitingToJump = false;
            if (IsGrounded())
            {
                _rb.AddForce(new Vector2(0, JumpStrength), ForceMode2D.Impulse);
                _speed = 0; // speed uniformisation on jump
            }
        }

        if (IsGrounded() && _speed == 0f && Random.Range(0f, 1f) < SprintChancePerSecond * Time.deltaTime)
            StartSprint(Random.Range(MinSpeed, MaxSpeed));

        if ((_speed > 0f && transform.position.x > GetSprintDest())
            || (_speed < 0f && transform.position.x < GetSprintDest()))
            _speed = 0f;

        transform.Translate(new Vector2(_speed, 0f));
        animator.SetBool("IsGrounded", IsGrounded());
    }

    private float GetSprintDest()
    {
        return _sprintDest + _posCenter.position.x;
    }

    private void StartSprint(float speed)
    {
        if (transform.position.x > _posCenter.position.x)
        {
            _speed = -speed;
            _sprintDest = Random.Range(-2.5f, 0f);

            Debug.Assert(_speed < 0f);
        }
        else
        {
            _speed = speed;
            _sprintDest = Random.Range(0f, 2.5f);

            Debug.Assert(_speed > 0f);
        }
    }

    public void Jump(float x)
    {
        if (_isWaitingToJump)
            return;

        _jumpPos = x;
        _isWaitingToJump = true;
    }

    private bool IsGrounded()
    {
        return _rb.velocity.y == 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Child"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        else if (collision.collider.CompareTag("Obstacle"))
        {
            _speed = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SprintTrigger"))
        {
            StartSprint(MaxSpeed * 1.5f);
        }

        if (collision.CompareTag("StopTrigger"))
        {
            StartSprint(-MaxSpeed * 1.5f);
        }
    }

}
