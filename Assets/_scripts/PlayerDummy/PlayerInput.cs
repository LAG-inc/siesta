using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), DisallowMultipleComponent]
public class PlayerInput : MonoBehaviour
{
    public static PlayerInput SI;

    public bool IsMoving { get; private set; }
    public bool IsJumping { get; set; }

    private float _direction;

    [SerializeField, Tooltip("Angulo hacia el cual se mueve"), Range(-360, 360)]
    private float cameraAngle = 90;

    [SerializeField, Range(0, 20)] private float velocity;

    private Rigidbody2D _playerRb;

    private Animator _playerAnimator;

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
        _direction = Input.GetAxisRaw("Horizontal");
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -5.6f, 1.3f));
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _playerAnimator.SetTrigger("Jump");
            GameObject.Find("ShadowAnim").GetComponent<Animator>().SetTrigger("Jump");
        }
        GameObject.Find("Shadow").transform.position = new Vector3(GameObject.Find("Shadow").transform.position.x, 
                                                                    transform.position.y, 
                                                                    GameObject.Find("Shadow").transform.position.z);
        this.GetComponent<BoxCollider2D>().enabled = !IsJumping;
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