﻿using System;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    [SerializeField] private float velocity = 1.0f;
    [SerializeField] private Transform resetPoint;

    private void Awake()
    {
        resetPoint = resetPoint == null ? GameObject.Find("ResetPoint").gameObject.transform : resetPoint;
    }

    void FixedUpdate()
    {
        transform.Translate(velocity * Time.deltaTime, 0, 0);
        if (transform.position.x > resetPoint.position.x)
        {
            PatternManager.SI.ResetPatternValues(gameObject);
        }
    }
}