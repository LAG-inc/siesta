using System;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    [SerializeField] private float velocity = 1.0f;
    private Transform resetPoint;

    private void Awake()
    {
        resetPoint = resetPoint == null ? GameObject.Find("ResetPoint").gameObject.transform : resetPoint;
    }

    void FixedUpdate()
    {
        if (GameManager.SI.currentGameState != GameState.InGame) return;
        transform.Translate(velocity * Time.deltaTime, 0, 0);
        if (transform.position.x > resetPoint.position.x)
        {
            PatternManager.SI.ResetPatternValues(gameObject);
        }
    }

    public void SetPatternVelocity(float lVelocity)
    {
        velocity = lVelocity;
    }
}