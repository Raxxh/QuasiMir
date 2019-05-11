using UnityEngine;

public class Parralax : MonoBehaviour
{
    /// <summary>
    /// Unit by seconds
    /// </summary>
    public float Speed = 1f;

    void FixedUpdate()
    {
        transform.Translate(new Vector2(Speed * Time.deltaTime, 0));
    }
}
