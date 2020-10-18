using UnityEngine;

public class Phase : MonoBehaviour
{
    public float velocity = 1.0f;

    void FixedUpdate()
    {
        transform.Translate(velocity * Time.deltaTime, 0, 0);
    }
}
