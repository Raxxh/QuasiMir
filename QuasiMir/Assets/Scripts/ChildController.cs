using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildController : MonoBehaviour
{
    public static readonly float JumpStrength = 10f;
    public static readonly float Gravity = 15f;

    private float _posY;

    private bool _isWaitingToJump = false;
    private float _jumpPos;

    private bool _isJumping = false;
    private float _verticalSpeed;
    
    // Use this for initialization
    void Start()
    {
        _posY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isJumping)
        {
            _verticalSpeed -= Gravity * Time.deltaTime;
            Debug.Log("VerticalSpeed is " + _verticalSpeed);
            transform.Translate(new Vector2(0, _verticalSpeed) * Time.deltaTime);

            if (transform.transform.position.y < _posY)
            {
                _isJumping = false;
                transform.position = new Vector3(transform.position.x, _posY, transform.position.z);
            }
        }

        if (_isWaitingToJump && transform.position.x > _jumpPos)
        {
            _isWaitingToJump = false;
            _isJumping = true;
            _verticalSpeed = JumpStrength;
        }
    }

    public void Jump(float x)
    {
        if (_isJumping)
            return;

        _jumpPos = x;
        _isWaitingToJump = true;
        Debug.Log("I shall jump on x = " + x);
    }
}
