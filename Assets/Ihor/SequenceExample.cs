using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class SequenceExample : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _stepDuration = 1f;

    private int _counter = 0;

    private const float FIRST_POINT_Y = 3f;
    private const float FIRST_POINT_X = 1f;
    private Vector3 SECOND_POINT = new Vector3(-1f, -2f, 0);
    private Vector3 THIRD_POINT = new Vector3(2f, 3.5f, 0);

    void Start()
    {
        MoveSun();
    }

    private void MoveSun()
    {
        Sequence move = DOTween.Sequence();
        //move.PrependInterval(5f);
        move.Append(transform.DOMoveY(FIRST_POINT_Y, _stepDuration).SetEase(Ease.InQuint).OnComplete(DebugMoves));
        move.Join(transform.DOMoveX(FIRST_POINT_X, _stepDuration).SetEase(Ease.InQuint).OnComplete(DebugMoves));

        move.Append(transform.DOMove(SECOND_POINT, _stepDuration).SetEase(Ease.InQuint).OnComplete(DebugMoves));
        //move.AppendInterval(5f);
        move.Append(transform.DOMove(THIRD_POINT, _stepDuration).SetEase(Ease.InQuint).OnComplete(DebugMoves));
        move.OnComplete(RemoveObject);
        move.Play();
    }

    private void DebugMoves()
    {
        _counter++;
        _text.text = _counter.ToString();
    }

    private void RemoveObject()
    {
        Destroy(gameObject);
    }
}