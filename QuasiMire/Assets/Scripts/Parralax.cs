using UnityEngine;

public class Parralax : MonoBehaviour
{
    /// <summary>
    /// Unit by seconds
    /// </summary>
    public float Speed = 1f;

    public Transform Camera;

    public bool Loop = false;

    private GameObject _twin = null;

    private float width;

    private void Start()
    {
        if (Loop)
        {
            width = GetComponent<Renderer>().bounds.size.x;
            _twin = Instantiate(gameObject, new Vector3(transform.position.x + width, transform.position.y, transform.position.z), Quaternion.identity, transform.parent);

            var p = _twin.GetComponent<Parralax>();

            p.Speed = Speed;
            p.Loop = false;
        }
    }

    void FixedUpdate()
    {
        transform.Translate(new Vector2(-Speed * Time.deltaTime, 0));

        if (Loop)
        {
            CheckLoopObjectPos(gameObject);
            CheckLoopObjectPos(_twin);
        }
    }

    private void CheckLoopObjectPos(GameObject target)
    {
        if (target.transform.position.x < Camera.transform.position.x - (width / 2) - 20) // -20 for camera fov
            target.transform.Translate(new Vector2(width * 2, 0));
    }
}
