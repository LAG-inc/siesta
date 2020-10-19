using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public ScriptableObstacle obstacleValues;
    [HideInInspector] public bool canSumSpawn, canSumPlayZone;

    private void OnEnable()
    {
        canSumSpawn = true;
    }
}