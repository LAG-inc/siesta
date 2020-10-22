using System.Collections;
using UnityEngine;


public enum AlienType
{
    Pattern,
    Constant
}

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


    public AlienType currentType;

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
        SFXManager.SI.PlaySound(Sound.AlienComming);
    }

    private void FixedUpdate()
    {
        if (GameManager.SI.currentGameState == GameState.MainMenu ) return;

        if (_cPatrol == null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _destiny, velocity * Time.fixedDeltaTime);
        }

        if (_cPatrol == null && transform.position == _destiny)
            gameObject.SetActive(false);
    }

    public void RestartValues()
    {
        _cPatrol = null;
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
                yield return new WaitUntil(() => GameManager.SI.currentGameState != GameState.MainMenu);
            }

            _sprite.flipX = transform.position.x < PlayerInput.SI.gameObject.transform.position.x;

            _cExplosion = StartCoroutine(AttackExplosion());

            SFXManager.SI.PlaySound(Sound.AlienStay);

            yield return new WaitUntil(() => _cExplosion == null);
        }

        RestartValues();

        SFXManager.SI.PlaySound(Sound.AlienExit);
    }


    private IEnumerator AttackExplosion()
    {
        attackPoint.gameObject.SetActive(true);
        attackPoint.gameObject.transform.position = PlayerInput.SI.gameObject.transform.position;
        _currentShoots = currentType == AlienType.Pattern ? _currentShoots + 1 : _currentShoots;

        var currentExplosionTime = 0f;

        while (currentExplosionTime < explosionTime)
        {
            currentExplosionTime += Time.deltaTime;
            yield return new WaitUntil(() => GameManager.SI.currentGameState != GameState.MainMenu);
            yield return new WaitForEndOfFrame();
        }

        SFXManager.SI.PlaySound(Sound.Meteorite);
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
        _cPatrol = _cPatrol == null ? StartCoroutine(AlienPatrol()) : _cPatrol;
    }
}