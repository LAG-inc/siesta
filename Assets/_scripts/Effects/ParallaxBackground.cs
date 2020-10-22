using UnityEngine;

[DisallowMultipleComponent]
public class ParallaxBackground : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float initialBackgroundVelocity;

    [SerializeField, Tooltip("Entr    e mas grande mas lento crecer"), Range(0.1f, 20)]
    private float delayGrowSpeed;

    private float _sizeX;
    [SerializeField] private BoxCollider2D backgroundSprite;
    private Vector3 _initialPosition;
    private float _currentBackgroundVelocity;


    private void Awake()
    {
        _currentBackgroundVelocity = initialBackgroundVelocity;

        _sizeX = backgroundSprite.size.x;
        _initialPosition = transform.position;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(_currentBackgroundVelocity * Time.deltaTime, 0, 0);
        _currentBackgroundVelocity += Time.deltaTime / delayGrowSpeed;
        transform.position = transform.position.x > _initialPosition.x + _sizeX ? _initialPosition : transform.position;
    }
}