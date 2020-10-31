using System;
using UnityEditor.VersionControl;
using UnityEngine;

[DisallowMultipleComponent]
public class ParallaxBackground : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float initialBackgroundVelocity;

    [SerializeField, Tooltip("Entre mas grande mas lento crecer"), Range(0.1f, 20)]
    private float delayGrowSpeed;

    private float _sizeX;
    [SerializeField] private BoxCollider2D backgroundSprite;

    [SerializeField] private float offssetCorrection;

    private Vector3 _initialPosition;
    private float _currentBackgroundVelocity;


    private void Awake()
    {
        _currentBackgroundVelocity = initialBackgroundVelocity;
        _initialPosition = transform.position;

        _sizeX = transform.GetChild(0).GetComponent<Renderer>().bounds.size.x;

        if (transform.rotation.eulerAngles.z != 0)
            _sizeX = -Mathf.Cos(transform.rotation.eulerAngles.z) * _sizeX;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.SI.currentGameState != GameState.InGame) return;
        transform.Translate(_currentBackgroundVelocity * Time.deltaTime, 0, 0);
        _currentBackgroundVelocity += Time.deltaTime / delayGrowSpeed;
        transform.position =
            transform.position.x >= _initialPosition.x + _sizeX + offssetCorrection
                ? _initialPosition
                : transform.position;
    }
}