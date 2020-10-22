//Script encargado de mantener al player en su zona y darle animaciones


using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerAnimation : MonoBehaviour
{
    //Animacion de rotacion por codigo
    private float _maxY, _minY;
    private Collider2D _ground;


    //Animaciones con animator
    private Animator _playerAnimator;


    private Animator _shadowAnim;
    private GameObject _shadow;
    private GameObject _playerContainer;
    private SpriteRenderer _sprite;
    private static readonly int Jump = Animator.StringToHash("Jump");

    // A kind of singleton
    public static PlayerAnimation SI;


    private void Awake()
    {
        SI = SI == null ? this : SI;
        _sprite = GetComponent<SpriteRenderer>();
        _shadow = GameObject.Find("Shadow");
        _shadowAnim = GameObject.Find("ShadowAnim").GetComponent<Animator>();
        _playerContainer = GameObject.Find("PlayerContainer");
        _playerAnimator = GetComponentInParent<Animator>();
        _ground = GameObject.FindGameObjectWithTag("GroundPlayable").GetComponent<BoxCollider2D>();

        _maxY = _ground.bounds.max.y - _playerContainer.transform.position.y;
        _minY = _ground.bounds.min.y - _playerContainer.transform.position.y + .65f;
    }


    private void Update()
    {
        if (GameManager.SI.currentGameState != GameState.InGame) return;

        if (PlayerInput.SI.SpaceKey)
        {
            _playerAnimator.SetTrigger(Jump);
            _shadowAnim.SetTrigger(Jump);
            SFXManager.SI.PlaySound(Sound.salto);
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

    public void ToggleColorInvoke(float time)
    {
        StartCoroutine(ToggleColor(time));
    }


    public IEnumerator ToggleColor(float TogleTime)
    {
        var currentTIme = 0.0f;
        var spriteColor = _sprite.color;
        while (currentTIme < TogleTime)
        {
            currentTIme += 0.2f;
            _sprite.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, 0.5f);
            yield return new WaitForSeconds(0.1f);
            _sprite.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, 1);
            yield return new WaitForSeconds(0.1f);
        }

        _sprite.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, 1);
    }
}