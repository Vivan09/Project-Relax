using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class PuzzleDrag : MonoBehaviour
{
    [SerializeField] private float distance = 0.4f;
    private float _deltaStart = 2f;
    private float deltaDuration = 1f;

    private Vector3 _mousePos;

    private Vector2 oldPosition;
    private bool isPressedMouse;
    private bool isDrag = true;

    public AudioSource audioSource;
    [SerializeField] private ManagerPuzzle _managerPuzzle;

    private void OnMouseDown() {
        isPressedMouse = true;
    }

    private void OnMouseUp() {
        isPressedMouse = false;
    }

    private void Start()
    {
        oldPosition = transform.position;
        _managerPuzzle.NewPos += NewPosition;

        NewPosition();

        isDrag = true;
    }

    private void FixedUpdate() {
        if(isPressedMouse && isDrag){
            _mousePos = Input.mousePosition;
            _mousePos = Camera.main.ScreenToWorldPoint(_mousePos);
            transform.position = new Vector2(_mousePos.x, _mousePos.y);        
        }
        else if(isDrag){ // прикріпити пазл до початкової позиції
            if(Vector2.Distance(oldPosition, transform.position)<distance){
                transform.position = Vector2.MoveTowards(transform.position, oldPosition, 0.5f);
                audioSource.Play();
                isDrag = false;
            }
        }
    }

    private void NewPosition()
    {
        isDrag = false;
        var targetPosition = new Vector2(Random.Range(-2.35f, 2.22f) , Random.Range(-2.05f, -4.6f));
        var tweenCallback = transform.DOMove(targetPosition, deltaDuration).SetEase(Ease.InQuad).OnComplete(() =>
        {
            isDrag = true;
        });

        // transform.position = Vector2.MoveTowards(transform.position, targetPosition, 15f);
    }
}