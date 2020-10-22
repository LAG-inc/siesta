using System;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    [Tooltip("Lista de fases, el valor representa el número de patrones de cada fase")]
    public float initialTimeBetweenPhase;
    public List<int> phases;

    private float _timeBetweenPhase;
    private int _remainingPhase;
    private int _currentPhase = 0;
    private  bool _pause = false;
    private PatternManager _patternManager;
    public static PhaseManager SI;

    void Awake()
    {
        SI = SI == null ? this : SI;

        _remainingPhase = phases.Count;
        _patternManager = GameObject.FindObjectOfType<PatternManager>();
    }

    void Update()
    {
        if (_remainingPhase <= 0 || _pause) return;
        _timeBetweenPhase -= Time.deltaTime;
        if (_timeBetweenPhase <= 0 && _patternManager.finished) InitPhase();
    }

    void InitPhase()
    {
        Debug.Log("Phase: " + _currentPhase);
        _patternManager.remainingPattern = phases[_currentPhase++];
        _patternManager.finished = false;
        _remainingPhase--;
    }

    public void Pause(bool pause)
    {
        _pause = pause;
    }
}
