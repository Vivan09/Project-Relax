using System;
using UnityEngine;

public class FinishTriger : MonoBehaviour
{
    public event Action OnFinished;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnFinished?.Invoke();
        }
    }
}
