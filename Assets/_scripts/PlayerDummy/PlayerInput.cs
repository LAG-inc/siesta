/*Script encargado de recibir inputs, realizar las acciones basicas como movimiento y salto
ademas de definir booleanos para pasarselos al script de animaciones */

using UnityEngine;


public enum Controll
{
    Vertical,
    Horizontal
}

[RequireComponent(typeof(Rigidbody2D)), DisallowMultipleComponent]
public class PlayerInput : MonoBehaviour
{
    public static PlayerInput SI;

    //Inputs para modificar animaciones en el script PlayerAnimation
    public bool IsMoving { get; private set; }

    public bool IsJumping { get; set; }

    public float Direction { get; private set; }


    //Editor parameters
    [SerializeField, Tooltip("Angulo hacia el cual se mueve(Modificar en caso de cambiar la camara)"), Range(-360, 360)]
    private float cameraAngle = 90;

    [SerializeField, Range(0, 20)] private float velocity;

    private Rigidbody2D _playerRb;

    private Animator _playerAnimator;


    [Header("Control")] [SerializeField] private Controll currentControl;
    [SerializeField] private bool slipMode;
    private string _axisControl;

    private const string AxisHorizontal = "Horizontal";

    private const string AxisVertical = "Vertical";


    private void Awake()
    {
        SI = SI == null ? this : SI;
        _playerRb = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponentInParent<Animator>();
    }

    private void Start()
    {
        IsJumping = false;
        IsMoving = true;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -5.6f, 1.3f));

        //Refactorizar en player animation
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerAnimator.SetTrigger("Jump");
            GameObject.Find("ShadowAnim").GetComponent<Animator>().SetTrigger("Jump");
        }

        //Implementar en un script aparte
        GameObject.Find("Shadow").transform.position = new Vector3(GameObject.Find("Shadow").transform.position.x,
            transform.position.y,
            GameObject.Find("Shadow").transform.position.z);
        GetComponent<BoxCollider2D>().enabled = !IsJumping;


        switch (currentControl)
        {
            case Controll.Horizontal:
                _axisControl = AxisHorizontal;
                break;
            case Controll.Vertical:
                _axisControl = AxisVertical;
                break;
        }

        if (Input.GetAxisRaw(_axisControl) != 0)
        {
            Direction = Input.GetAxisRaw(_axisControl);
            IsMoving = true;
        }
        else
        {
            IsMoving = false;
            if (!slipMode)
                Direction = 0;
        }
    }

    private void FixedUpdate()
    {
        IsMoving = Direction != 0;
        _playerRb.velocity = IsMoving
            ? new Vector2(Util.GetVectorFromAngle(cameraAngle).x * velocity * Direction,
                Util.GetVectorFromAngle(cameraAngle).y * velocity * Direction)
            : Vector2.zero;
    }
}
//Todo: Implementar salto, triggers implementar en stats(Trigger de win, choque,etc (En stats ya que ahi maneja la cantidad de vidas y es lo mas optimo a mi parecer))