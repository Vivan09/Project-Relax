using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ShowDebug : MonoBehaviour
{
    [SerializeField] private Button _close;
    [SerializeField] private TMP_Text _textBox;

    private bool _showed = false;
    private Vector3 _startPosition;

    private const float MOVE_DURATION = 0.5f;
    private const float TARGET_Y_POSITION = 500f;

    private void Start()
    {
        _close?.onClick.AddListener(ShowClose);
    }

    public void Write(string message)
    {
        _textBox.text += '\n' + message; ;
    }

    public void ShowClose()
    {
        if (_startPosition == Vector3.zero)
        {
            _startPosition = transform.position;
        }

        if (_showed)
        {
            transform.DOMove(_startPosition, MOVE_DURATION);
        }
        else
        {
            transform.DOMoveY(transform.position.y + TARGET_Y_POSITION, MOVE_DURATION);
        }
        _showed = !_showed;
    }

    private void OnDestroy()
    {
        _close?.onClick.RemoveAllListeners();
    }
}
