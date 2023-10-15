using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DOTweenExample : MonoBehaviour
{
    [SerializeField] private float _stepDuration = 1f;

    private const float FIRST_POINT_Y = 3f;
    private Vector3 SECOND_POINT = new Vector3(-1f, -2f, 0);

    void Start()
    {
        MoveSun();
    }

    private void MoveSun()
    {
        MoveUp();
    }

    private void MoveUp()
    {
        transform.DOMoveY(FIRST_POINT_Y, _stepDuration).SetEase(Ease.InQuint)
            .OnComplete(MoveDown);
    }

    private void MoveDown()
    {
        transform.DOMove(SECOND_POINT, _stepDuration).SetEase(Ease.InQuint);
    }
}
