﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), DisallowMultipleComponent]
public class PlayerInput : MonoBehaviour
{
    private static PlayerInput SI;

    public bool IsMoving { get; private set; }
    public bool IsJumping { get; private set; }

    private float _direction;

    [SerializeField, Tooltip("Angulo hacia el cual se mueve"), Range(-360, 360)]
    private float cameraAngle = 90;

    [SerializeField, Range(0, 20)] private float velocity;

    private Rigidbody2D _playerRb;

    private void Awake()
    {
        SI = SI == null ? this : SI;
        _playerRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        IsJumping = false;
        IsMoving = true;
    }

    private void Update()
    {
        _direction = Input.GetAxisRaw("Horizontal");
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -5.6f, 1.3f));
    }

    private void FixedUpdate()
    {
        IsMoving = _direction != 0;
        _playerRb.velocity = IsMoving
            ? new Vector2(Util.GetVectorFromAngle(cameraAngle).x * velocity * _direction,
                Util.GetVectorFromAngle(cameraAngle).y * velocity * _direction)
            : Vector2.zero;
    }
}