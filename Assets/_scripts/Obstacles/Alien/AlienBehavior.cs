using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AlienBehavior : MonoBehaviour
{
    [Header("Behavior vars")] [SerializeField, Range(0, 10)]
    private float velocity;

    [SerializeField, Range(0, 5)] private float timeBetweenShot;
    [SerializeField, Range(0, 5)] private int shoots;
    private List<GameObject> _projectiles = new List<GameObject>();
    private List<GameObject> _projectilesInGame = new List<GameObject>();

    [Header("Points pattern")] [SerializeField]
    private Transform[] patternPoints;

    //Pattern vars
    private int _currentShoots;
    private int _currentPattern;
    private bool _goOn;
    private Coroutine _cGoAttack;

    [SerializeReference, Tooltip("Ingresar al padre de los projectiles")]
    private GameObject parentProjectiles;

    //Move vars
    private Vector3 _destiny;
    private Vector3 _initialPosition;


    private void Awake()
    {
        timeBetweenShot = timeBetweenShot > 0 ? timeBetweenShot : 2;
        for (var i = 0; i < parentProjectiles.transform.childCount; i++)
        {
            if (parentProjectiles.transform.GetChild(i).CompareTag("Obstacle"))
            {
                _projectiles.Add(parentProjectiles.transform.GetChild(i).gameObject);
            }
        }

        _goOn = true;
        _initialPosition = transform.position;
        _currentShoots = 0;
    }


    private void FixedUpdate()
    {
        if (_cGoAttack == null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _destiny, velocity * Time.fixedDeltaTime);
        }

        if (_cGoAttack == null && transform.position == _destiny && _projectilesInGame.Count == 0)
            gameObject.SetActive(false);
    }

    private void Attack()
    {
        var currentProjectile = _projectiles.Count > 1 ? Random.Range(0, _projectiles.Count) : 0;
        var projectile = _projectiles[currentProjectile];


        projectile.gameObject.SetActive(true);
        _projectiles.Remove(projectile);
        _projectilesInGame.Add(projectile);
        _currentShoots++;
    }

    private void RestartValues()
    {
        _destiny = _initialPosition + new Vector3(0, 10f);
        _currentShoots = 0;
    }

    private IEnumerator AlienToAttack()
    {
        while (_currentShoots < shoots)

        {
            SetDestiny();

            while (transform.position != _destiny)
            {
                transform.position = Vector3.MoveTowards(transform.position, _destiny,
                    velocity * Time.fixedDeltaTime);

                yield return new WaitForFixedUpdate();
                /*Sustituir por --> ()=>currentGameState=GameState.inGame   
                yield return new WaitUntil(() => true);*/
            }

            Attack();
            yield return new WaitForSeconds(timeBetweenShot);
        }

        RestartValues();
        _cGoAttack = null;
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
        foreach (var projectile in _projectiles)
        {
            projectile.SetActive(false);
        }

        _cGoAttack = _cGoAttack ?? StartCoroutine(AlienToAttack());
    }


    public void ProjectileDestroy(GameObject projectile)
    {
        _projectilesInGame.Remove(projectile);
        _projectiles.Add(projectile);
    }
}