//Script encargado de mantener al player en su zona y darle animaciones

using System;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerAnimation : MonoBehaviour
{
    //Animacion de rotacion por codigo
    private float _maxY, _minY;
    private Collider2D _ground;
    [SerializeField] private float offsetClamp;

    //Animaciones con animator
    private Animator _animator;


    // A kind of singleton
    public static PlayerAnimation SI;

    private void Awake()
    {
        SI = SI == null ? this : SI;
        _animator = GetComponent<Animator>();
        _ground = GameObject.FindGameObjectWithTag("GroundPlayable").GetComponent<BoxCollider2D>();
        _maxY = _ground.bounds.max.y - offsetClamp;

        _minY = _ground.bounds.min.y + offsetClamp;
    }


    private void FixedUpdate()
    {
        //Para que sea mas entendible no lo metodo con operador ternario  --->  ? : ; 
        if (Math.Abs(transform.position.y - _maxY) < Mathf.Epsilon ||
            Math.Abs(transform.position.y - _minY) < Mathf.Epsilon)
            transform.rotation = Quaternion.Euler(new Vector3(0, 180f, 30f));
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180f, 30f + PlayerInput.SI.Direction * 30f));
        }
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _minY, _maxY));
    }
}