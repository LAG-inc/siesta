using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    public GameObject obstacleBase;
    public int maxObstacles = 10;

    private Queue<GameObject> _queue;

    void Awake()
    {
        _queue = new Queue<GameObject>(maxObstacles);
        PopulateQueue();
    }

    void PopulateQueue()
    {
        for (int i = 0; i < maxObstacles; i++)
        {
            AddNewObstacle();
        }
    }

    void AddNewObstacle()
    {
        GameObject newObstacle = Instantiate(obstacleBase);
        newObstacle.SetActive(false);
        _queue.Enqueue(newObstacle);
    }

    void SpawnFromQueue(SpawnPoint spawnPoint)
    {
        // En caso de no tener ningún objeto que sacar de la Queue, se instancia uno nuevo.
        if (_queue.Count == 0)
            AddNewObstacle();
        GameObject obstacle = _queue.Dequeue();
        obstacle.transform.SetParent(spawnPoint.transform);
        obstacle.transform.position = spawnPoint.transform.position;
        SetObstacleValues(obstacle, spawnPoint.obstacleValues);
        obstacle.SetActive(true);
    }

    void ReturnToQueue(GameObject obstacle)
    {
        obstacle.SetActive(false);
        _queue.Enqueue(obstacle);
    }

    void SetObstacleValues(GameObject obstacle, ScriptableObstacle obstacleValues)
    {
        SpriteRenderer spRenderer =obstacle.GetComponent<SpriteRenderer>();
        spRenderer.color = obstacleValues.color;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("SpawnPoint"))
        {
            SpawnFromQueue(other.gameObject.GetComponent<SpawnPoint>());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            ReturnToQueue(other.gameObject);
        }
    }
}
