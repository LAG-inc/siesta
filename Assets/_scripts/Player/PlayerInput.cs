/*Script encargado de recibir inputs, realizar las acciones basicas como movimiento y salto
ademas de definir booleanos para pasarselos al script de animaciones */

using UnityEngine;


public enum Controll
{
    Vertical,
    Horizontal
}

[DefaultExecutionOrder(-100), RequireComponent(typeof(Rigidbody2D)), DisallowMultipleComponent]
public class PlayerInput : MonoBehaviour
{
    public static PlayerInput SI;

    //Inputs para modificar animaciones en el script PlayerAnimation

    public bool IsMoving { get; private set; }
    public bool IsJumping { get; set; }
    public bool SpaceKey { get; private set; }
    private float _direction;


    //Movimiento
    [SerializeField, Tooltip("Angulo hacia el cual se mueve(Modificar en caso de cambiar la camara)"), Range(-360, 360)]
    private float cameraAngle = 90;

    [SerializeField, Range(0, 20)] private float velocity;
    private Rigidbody2D _playerRb;

    private BoxCollider2D _collider;

    //Tipo de control
    [Header("Control")] [SerializeField] private Controll currentControl;
    [SerializeField] private bool slipMode;
    private string _axisControl;
    private const string AxisHorizontal = "Horizontal";
    private const string AxisVertical = "Vertical";


    private void Awake()
    {
        SI = SI == null ? this : SI;
        _collider = GetComponent<BoxCollider2D>();
        _playerRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        IsJumping = false;
        IsMoving = true;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -5.6f, 1.3f));
        switch (currentControl)
        {
            case Controll.Horizontal:
                _axisControl = AxisHorizontal;
                break;
            case Controll.Vertical:
                _axisControl = AxisVertical;
                break;
        }

        //ReadInputs
        SpaceKey = Input.GetKeyDown(KeyCode.Space) && !IsJumping;
        if (Input.GetAxisRaw(_axisControl) != 0)
        {
            _direction = Input.GetAxisRaw(_axisControl);
            IsMoving = true;
        }
        else
        {
            IsMoving = false;
            if (!slipMode)
                _direction = 0;
        }

        _collider.enabled = !IsJumping;
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