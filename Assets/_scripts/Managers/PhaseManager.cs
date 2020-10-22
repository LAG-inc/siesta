using System.Collections.Generic;
using UnityEngine;


public class PhaseManager : MonoBehaviour
{
    public static PhaseManager SI;

    public float initialTimeBetweenPhase;

    [Tooltip("Lista de fases, el valor representa el número de patrones de cada fase")]
    public List<int> phases;

    private float _timeBetweenPhase;
    public int RemainingPhases { private set; get; }
    private int _currentPhase = 0;
    private bool _pause = false;
    private PatternManager _patternManager;


    void Awake()
    {
        SI = SI == null ? this : SI;
        RemainingPhases = phases.Count;
        _patternManager = FindObjectOfType<PatternManager>();
    }

    private void Start()
    {
        PhaseConfigurator.SI.SetPhaseConfig();
    }

    void Update()
    {
        if (RemainingPhases <= 0 || _pause) return;
        _timeBetweenPhase -= Time.deltaTime;
        if (_timeBetweenPhase <= 0 && _patternManager.finished)
        {
            InitPhase();
            PhaseConfigurator.SI.SetPhaseConfig();
        }
    }

    void InitPhase()
    {
        Debug.Log("Phase: " + _currentPhase);
        _patternManager.remainingPattern = phases[_currentPhase++];
        _patternManager.finished = false;
        RemainingPhases--;
    }

    public void Pause(bool pause)
    {
        _pause = pause;
    }

    public void ControlBetweenPhases(bool pause)
    {

        if (pause)
        {
            GameObject.Find("Alien").GetComponent<AlienBehavior>().RestartValues();
        }
        else
        {
            _patternManager.finished = true;
        }
    }
    public int GetCurrentPhase()
    {
        return _currentPhase;
    }
}