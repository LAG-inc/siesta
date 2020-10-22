using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesController : MonoBehaviour
{
    private List<GameObject> _aliens = new List<GameObject>();
    private List<GameObject> _meteorites = new List<GameObject>();
    public static EnemiesController SI;


    private void Awake()
    {
        SI = SI == null ? this : SI;
        foreach (var Alien in FindObjectsOfType<AlienBehavior>())
        {
            _aliens.Add(Alien.gameObject);
            Alien.gameObject.SetActive(false);
        }

        foreach (var meteorite in FindObjectsOfType<Meteorite>())
        {
            _meteorites.Add(meteorite.gameObject);
        }
    }


    public void AlienAttack()
    {
        var alienIndex = Random.Range(0, _aliens.Count);
        _meteorites[alienIndex].SetActive(true);
    }

    public void MeteoriteAttack()
    {
        var meteoriteIndex = Random.Range(0, _aliens.Count);
        _meteorites[meteoriteIndex].SetActive(true);
    }

    public void AlienBack()
    {
        foreach (var alien in _aliens.Where(alien => alien.activeSelf))
        {
            alien.GetComponent<AlienBehavior>().RestartValues();
        }
    }
}