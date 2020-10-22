using System.Collections;
using UnityEngine;

public class AlienBehavior : MonoBehaviour
{
    [Header("Behavior vars")] [SerializeField, Range(0, 10)]
    private float velocity;

    [SerializeField, Range(0, 5)] private float explosionTime;
    [SerializeField, Range(0, 5)] private int shoots;
    [SerializeField] private AttackPoint attackPoint;

    [Header("Points pattern")] [SerializeField]
    private Transform[] patternPoints;

    //Pattern vars
    private int _currentShoots;
    private int _currentPattern;
    private bool _goOn;
    private Coroutine _cPatrol;
    private Coroutine _cExplosion;

    //Move vars
    private Vector3 _destiny;
    private Vector3 _initialPosition;
    private SpriteRenderer _sprite;

    private void Awake()
    {
        explosionTime = explosionTime > 0 ? explosionTime : 3;
        _sprite = GetComponent<SpriteRenderer>();
        _goOn = true;
        _initialPosition = transform.position;
        _currentShoots = 0;
    }

    private void Start()
    {
        SFXManager.SI.PlaySound(Sound.ovniLlegada);
    }

    private void FixedUpdate()
    {
        if (_cPatrol == null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _destiny, velocity * Time.fixedDeltaTime);
        }

        if (_cPatrol == null && transform.position == _destiny)
            gameObject.SetActive(false);
    }

    private void RestartValues()
    {
        _destiny = _initialPosition + new Vector3(0, 10f);
        _currentShoots = 0;
    }

    private IEnumerator AlienPatrol()
    {
        while (_currentShoots < shoots)

        {
            SetDestiny();

            while (transform.position != _destiny)
            {
                transform.position = Vector3.MoveTowards(transform.position, _destiny,
                    velocity * Time.fixedDeltaTime);
                _sprite.flipX = transform.position.x < _destiny.x;
                yield return new WaitForFixedUpdate();
                /*Sustituir por --> ()=>currentGameState=GameState.inGame   
                yield return new WaitUntil(() => true);*/
            }

            _sprite.flipX = transform.position.x < PlayerInput.SI.gameObject.transform.position.x;


            _cExplosion = StartCoroutine(AttackExplosion());
            SFXManager.SI.PlaySound(Sound.ovniDetenido);
            yield return new WaitUntil(() => _cExplosion == null);
        }

        RestartValues();
        _cPatrol = null;
        SFXManager.SI.PlaySound(Sound.ovniSalida);

    }


    private IEnumerator AttackExplosion()
    {
        attackPoint.gameObject.SetActive(true);
        attackPoint.gameObject.transform.position = PlayerInput.SI.gameObject.transform.position;
        _currentShoots++;
        yield return new WaitForSeconds(explosionTime);
        SFXManager.SI.PlaySound(Sound.meteorito);
        attackPoint.Explosion();
        _cExplosion = null;
    }


    private void SetDestiny()
    {
        _destiny = patternPoints[_currentPattern].transform.position;

        if (_goOn && _currentPattern == patternPoints.Length - 1)
            _goOn = false;
        else if (!_goOn && _currentPattern == 0)
            _goOn = true;

        _currentPattern = _goOn ? _currentPattern + 1 : _currentPattern - 1;
    }

    private void OnEnable()
    {
        _cPatrol = _cPatrol ?? StartCoroutine(AlienPatrol());
    }
}