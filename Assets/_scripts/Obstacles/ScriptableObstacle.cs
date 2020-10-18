using UnityEngine;

[CreateAssetMenu(fileName = "Obstacle", menuName = "ScriptableObjects/Obstacle", order = 1)]
public class ScriptableObstacle : ScriptableObject
{
    public string obstacleName;
    public Color32 color;
}
