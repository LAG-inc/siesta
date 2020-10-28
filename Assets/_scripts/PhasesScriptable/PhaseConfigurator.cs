using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[DefaultExecutionOrder(500)]
public class PhaseConfigurator : MonoBehaviour
{
    private List<GameObject> _meteorites = new List<GameObject>();
    private List<GameObject> _patterns = new List<GameObject>();
    public List<PhaseConfig> phaseConfig = new List<PhaseConfig>();
    public static PhaseConfigurator SI;
    [SerializeField] private GameObject _alien;
    [SerializeField] private GameObject background;
    private int objPerMet;
    public bool isAttacking;

    private void Awake()
    {
        SI = SI == null ? this : SI;

        foreach (var pattern in GameObject.FindGameObjectsWithTag("Pattern"))
        {
            _patterns.Add(pattern);
        }

        foreach (var meteorite in FindObjectsOfType<Meteorite>())
        {
            meteorite.gameObject.SetActive(false);
            _meteorites.Add(meteorite.gameObject);
        }
    }

    private void Update()
    {
    }


    public void MeteoriteAttack()
    {
        isAttacking = true;
        var meteoriteIndex = Random.Range(0, _meteorites.Count);
        _meteorites[meteoriteIndex].SetActive(true);
    }

    public void SetPhaseConfig()
    {
        SetPatternVelocity();

        SetAlienBehavior();

        SetBackgroundBehavior();
    }


    private void SetPatternVelocity()
    {
        foreach (var pattern in _patterns)
        {
            pattern.GetComponent<Pattern>().velocity = phaseConfig[PhaseManager.SI.GetCurrentPhase()].patternVelocity;
        }
    }

    private void SetAlienBehavior()
    {
        _alien.SetActive(phaseConfig[PhaseManager.SI.GetCurrentPhase()].alien);
        SFXManager.SI.PlaySound(Sound.AlienComming);
        _alien.GetComponent<AlienBehavior>().currentType = phaseConfig[PhaseManager.SI.GetCurrentPhase()].alienType;
    }

    private void SetBackgroundBehavior()
    {
        background.GetComponent<Fade>().enabled = phaseConfig[PhaseManager.SI.GetCurrentPhase()].fade;
        background.GetComponent<MixColor>().enabled = phaseConfig[PhaseManager.SI.GetCurrentPhase()].mixColors;
    }
}
