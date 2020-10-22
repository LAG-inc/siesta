using System.Collections;
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


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Obstacle") && !other.CompareTag("Meteorite")) return;

        if (_currentInmTime < immuneTime) return;

        if (other.CompareTag("Obstacle")) SFXManager.SI.PlaySound(Sound.ObjectHit);

        _currentLife--;


        _currentInmTime = 0;

        Debug.Log("Hit");


        if (_currentLife > 0) PlayerAnimation.SI.ToggleColorInvoke(immuneTime);

        else Die();
    }


    private void Die()
    {
        onDie.Invoke();
        GameManager.SI.ChangeGameState(GameState.GameOver);
    }

    public void Respawn()
    {
        _currentLife = initialLife;
        PlayerAnimation.SI.RestartValues();
    }
}