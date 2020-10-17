using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //Use getter y setter en lugar de props para que unicamente reste o sume de uno en uno
    [SerializeField, Range(0, 10)] private int initialLife;
    private int _currentLife;
    private int _currentCollectable;


    //Llamar en otras clases sin referenciar
    public static PlayerStats SI;

    private void Awake()
    {
        _currentLife = initialLife;
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

    public int GetCurrentCollectable()
    {
        return _currentCollectable;
    }

    public void WinCollectable()
    {
        _currentCollectable++;
    }
}