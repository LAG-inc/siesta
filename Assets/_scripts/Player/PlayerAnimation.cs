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
    private GameObject _playerContainer;

    private static readonly int Jump = Animator.StringToHash("Jump");

    // A kind of singleton
    public static PlayerAnimation SI;

    private void Awake()
    {
        _shadow = GameObject.Find("Shadow");
        _shadowAnim = GameObject.Find("ShadowAnim").GetComponent<Animator>();
        _playerContainer = GameObject.Find("PlayerContainer");
        SI = SI == null ? this : SI;
        _playerAnimator = GetComponentInParent<Animator>();

        _ground = GameObject.FindGameObjectWithTag("GroundPlayable").GetComponent<BoxCollider2D>();
        _maxY = _ground.bounds.max.y - _playerContainer.transform.position.y - offsetClamp ;

        //.65f es lo que "mide" el sprite del borde superior al inferior en movimiento vertical,
        //intente hacerlo con el bound del collider pero no funcionó
        _minY = _ground.bounds.min.y - _playerContainer.transform.position.y + offsetClamp + .65f ;

    }


    private void Update()
    {
        if (PlayerInput.SI.SpaceKey)
        {
            _playerAnimator.SetTrigger(Jump);
            _shadowAnim.SetTrigger(Jump);
        }

        transform.localPosition = new Vector3(transform.localPosition.x, 
                    Mathf.Clamp(transform.localPosition.y, _minY, _maxY));

        if (PlayerInput.SI.IsJumping)
        {
            //Distancia entre posicion global y posicion local
            float dis = -6.8f;
            _shadow.transform.position = new Vector3(_shadow.transform.position.x,
                                                    transform.localPosition.y + dis,
                                                    _shadow.transform.position.z);
        }
        else
        {
            _shadow.transform.position = new Vector3(_shadow.transform.position.x,
                                                    transform.position.y,
                                                    _shadow.transform.position.z);
        }
    }
    
    
    
    
}