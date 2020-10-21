using System;
using UnityEngine;
using UnityEngine.Events;


[DisallowMultipleComponent]
public class PlayerStats : MonoBehaviour
{
    //Use getter y setter en lugar de props para que unicamente reste o sume de uno en uno
    [SerializeField, Range(0, 10)] private int initialLife;
    [SerializeField, Range(0.5f, 3f)] private float immuneTime;
    private float _currentInmTime;
    private int _currentLife;
    private int _currentCollectable;
    public UnityEvent onDie;

    //Llamar en otras clases sin referenciar
    public static PlayerStats SI;

    private void Awake()
    {
        _currentInmTime = immuneTime;
        _currentLife = initialLife;
        SI = SI == null ? this : SI;
    }

    private void Update()
    {
        /*
                if (GameManager.SI.currentGameState != GameState.InGame) return;*/
        if (_currentInmTime < immuneTime)
        {
            _currentInmTime += Time.deltaTime;
        }
    }


    /// <summary>
    /// Resta o suma la vida del player
    /// </summary>
    /// <param name="lostLife">True si resta vida, false si suma</param>
    public void ChangeLife(bool lostLife)
    {
        _currentLife = lostLife ? _currentLife-- : _currentLife++;
    }

    public int GetCurrentLife()
    {
        return _currentLife;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Obstacle")) return;
        if (_currentInmTime < immuneTime) return;
        _currentInmTime = 0;
        Debug.Log("Hit");
        ChangeLife(true);

        if (_currentLife > 0) return;
        GameManager.SI.currentGameState = GameState.GameOver;
        onDie.Invoke();
    }
}