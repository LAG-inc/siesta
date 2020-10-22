using UnityEngine;

[CreateAssetMenu(fileName = "Obstacle", menuName = "ScriptableObjects/Obstacle", order = 1)]
public class ScriptableObstacle : ScriptableObject
{
    public string obstacleName;
    public Sprite sprite;
    public bool flipX;
    public float colliderX;
    public float colliderY;
    public Quaternion rotation;
}
