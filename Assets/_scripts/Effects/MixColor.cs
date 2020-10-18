using UnityEngine;
using Random = UnityEngine.Random;

[DisallowMultipleComponent]
public class MixColor : EffectBase
{
    [SerializeField] private Color[] colors;


    private Color _currentColor;


    protected override void Effect()
    {
        //Asegura que el nuevo color no sea identico al que actualmente se usa
        while (true)
        {
            var newColor = colors[Random.Range(0, sprites.Length)];
            if (_currentColor == newColor) continue;
            _currentColor = newColor;
            break;
        }

        foreach (var sprite in sprites)
        {
            sprite.color = _currentColor;
        }
    }
}