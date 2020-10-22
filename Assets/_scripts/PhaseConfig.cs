using UnityEngine;


[CreateAssetMenu(fileName = "Obstacle", menuName = "ScriptableObjects/PhaseConfig", order = 1)]
public class PhaseConfig : ScriptableObject
{
    public bool fade;
    public bool mixColors;
    public float patternVelocity;
    public int metPerPhase;
    public bool alien;
    public AlienType alienType;
}