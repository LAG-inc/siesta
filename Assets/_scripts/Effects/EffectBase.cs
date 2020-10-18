using UnityEngine;


public abstract class EffectBase : MonoBehaviour
{
    [SerializeField, Tooltip("Desactivar y activar tras modificar en juego"), Range(0, 2)]
    private float timeBetweenEffect;
    [SerializeField] protected SpriteRenderer[] sprites;



    protected virtual void Effect()
    {
    }

    private void OnDisable()
    {
        CancelInvoke();
        foreach (var sprite in sprites)
        {
            sprite.color = Color.white;
        }
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(Effect), 0, timeBetweenEffect);
    }
}