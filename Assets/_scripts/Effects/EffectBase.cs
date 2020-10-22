using System;
using UnityEngine;


public abstract class EffectBase : MonoBehaviour
{
    [SerializeField, Tooltip("Desactivar y activar tras modificar en juego"), Range(0, 2)]
    private float timeBetweenEffect;

    [SerializeField] protected SpriteRenderer[] sprites;


    private float _currentTime;

    protected virtual void Effect()
    {
    }

    private void Update()
    {
        if (GameManager.SI.currentGameState != GameState.InGame) return;

        _currentTime += Time.deltaTime;

        if (!(_currentTime >= timeBetweenEffect)) return;

        _currentTime = 0;

        Effect();
    }

    private void OnDisable()
    {
        foreach (var sprite in sprites)
        {
            sprite.color = Color.white;
        }
    }
}