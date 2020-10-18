//Script encargado de mantener al player en su zona y darle animaciones


using UnityEngine;

[DisallowMultipleComponent]
public class PlayerAnimation : MonoBehaviour
{
    //Animacion de rotacion por codigo
    private float _maxY, _minY;
    private Collider2D _ground;
    [SerializeField] private float offsetClamp;

    //Animaciones con animator
    private Animator _playerAnimator;


    private Animator _shadowAnim;
    private GameObject _shadow;

    private static readonly int Jump = Animator.StringToHash("Jump");

    // A kind of singleton
    public static PlayerAnimation SI;

    private void Awake()
    {
        _shadow = GameObject.Find("Shadow");
        _shadowAnim = GameObject.Find("ShadowAnim").GetComponent<Animator>();
        SI = SI == null ? this : SI;
        _playerAnimator = GetComponentInParent<Animator>();

        _ground = GameObject.FindGameObjectWithTag("GroundPlayable").GetComponent<BoxCollider2D>();
        _maxY = _ground.bounds.max.y - offsetClamp;
        _minY = _ground.bounds.min.y + offsetClamp;
    }


    private void Update()
    {
        if (PlayerInput.SI.SpaceKey)
        {
            _playerAnimator.SetTrigger(Jump);
            _shadowAnim.SetTrigger(Jump);
        }

        _shadow.transform.position = new Vector3(_shadow.transform.position.x,
            Mathf.Clamp(transform.position.y, _minY, _maxY),
            _shadow.transform.position.z);


        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _minY, _maxY));
    }
}