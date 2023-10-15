using System.Collections;
using UnityEngine;

public class SceneMovementScr : MonoBehaviour
{
    [SerializeField] private float _speed = 2.5f;
    [SerializeField] private float _targetY = 2f;

    private Coroutine _moving;

    private const float MAX_LERP_T = 1f;

    public void StartMove(string tag)
    {
        if (_moving == null)
        {
            _moving = StartCoroutine(Move());
        }
    }

    private IEnumerator Move()
    {
        float time = 0;
        Vector3 targetPosition = transform.position + (Vector3.up * _targetY);

        while (time < MAX_LERP_T)
        {
            time += Time.fixedDeltaTime * _speed;
            transform.position = Vector3.Lerp(transform.position, targetPosition, time);

            yield return new WaitForFixedUpdate();
        }

        _moving = null;
    }
}