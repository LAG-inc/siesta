using UnityEngine;


public abstract class EffectBase : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer[] sprites;

    [Header("Not Modifiable In Game"), SerializeField, Tooltip("Reiniciar para modificar"), Range(0, 2)]
    protected float timeBetweenEffect;


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