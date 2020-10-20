using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[DisallowMultipleComponent, DefaultExecutionOrder(100)]
public class SpawnManager : MonoBehaviour
{
    public static SpawnManager SI;
    private List<GameObject> _patterns = new List<GameObject>();
    private List<GameObject> _patternsRunning = new List<GameObject>();


    private bool _canRun;
    private int _childCount;
    private int _childToRun;


    private float _timeBetweenPattern;
    [SerializeField] private float initialTimeBetweenPattern;


    private Vector3 _initialPositionPat;

    private void Awake()
    {
        _initialPositionPat = transform.position - new Vector3(2f, 0);
        SI = SI == null ? this : SI;
        foreach (var pattern in GameObject.FindGameObjectsWithTag("Pattern"))
        {
            _patterns.Add(pattern);
        }

        _canRun = true;
    }

    private void Start()
    {
        foreach (var pattern in _patterns)
        {
            pattern.SetActive(false);
            pattern.transform.position = _initialPositionPat;
        }

        GetComponent<Collider2D>().enabled = true;
    }

    private void Update()
    {
        if (!_canRun) return;
        _timeBetweenPattern -= Time.deltaTime;
        if (_timeBetweenPattern <= 0) InitParent();
    }

    private void InitParent()
    {
        ResetSpawnValues();

        var pattern = Random.Range(0, _patterns.Count);
        var currentPat = _patterns[pattern];

        _patterns.Remove(currentPat);
        _patternsRunning.Add(currentPat);

        currentPat.SetActive(true);

        _childToRun = 0;
        for (var i = 0; i < currentPat.transform.childCount; i++)
        {
            if (currentPat.transform.GetChild(i).gameObject.layer == LayerMask.NameToLayer("SpawnPoint"))
                _childToRun++;
        }
    }


    private void ResetSpawnValues()
    {
        _childCount = 0;
        _canRun = false;
        _timeBetweenPattern = initialTimeBetweenPattern;
    }

    public void ResetPatternValues(GameObject lPattern)
    {
        lPattern.gameObject.SetActive(false);
        lPattern.transform.position = _initialPositionPat;

        try
        {
            _patternsRunning.Remove(lPattern);
            _patterns.Add(lPattern);
        }
        catch
        {
            Debug.Log("Pattern no agregado a la lista de patternsRunning ----> arreglar");
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("SpawnPoint")) return;
        if (!other.gameObject.GetComponent<SpawnPoint>().canSumSpawn) return;
        other.gameObject.GetComponent<SpawnPoint>().canSumSpawn = false;
        _childCount++;
        _canRun = _childCount >= _childToRun;
    }
}