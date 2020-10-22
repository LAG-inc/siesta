using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[DisallowMultipleComponent, DefaultExecutionOrder(100)]
public class PatternManager : MonoBehaviour
{
    public static PatternManager SI;

    [Tooltip("Número de patrones restantes de la phase")]
    public int remainingPattern;

    public bool finished = false;
    private List<GameObject> _patterns = new List<GameObject>();
    private List<GameObject> _patternsRunning = new List<GameObject>();


    private bool _canRun;
    public int ChildCount { private set; get; }
    public int ChildToRun { private set; get; }


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
        if (GameManager.SI.currentGameState != GameState.InGame) return;

        if (!_canRun) return;

        if (remainingPattern <= 0)
        {
            //finished = true;
            UIManager.SI.PlayTimeLineAttemps(100);
            return;
        }

        _timeBetweenPattern -= Time.deltaTime;

        if (_timeBetweenPattern <= 0) InitPattern();
    }

    private void InitPattern()
    {
        ResetSpawnValues();

        var pattern = Random.Range(0, _patterns.Count);
        var currentPat = _patterns[pattern];

        _patterns.Remove(currentPat);
        _patternsRunning.Add(currentPat);

        currentPat.SetActive(true);

        ChildToRun = 0;
        for (var i = 0; i < currentPat.transform.childCount; i++)
        {
            if (currentPat.transform.GetChild(i).gameObject.layer == LayerMask.NameToLayer("SpawnPoint"))
                ChildToRun++;
        }

        PhaseConfigurator.SI.MeteoriteAttack();
        remainingPattern--;
        Debug.Log("Patron: " + remainingPattern);
    }


    private void ResetSpawnValues()
    {
        ChildCount = 0;
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
        ChildCount++;
        _canRun = ChildCount >= ChildToRun;
    }

}