using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class CardQuoteItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _button;

    public void SetClickEvent(UnityAction onClick)
    {
        _button.onClick.AddListener(onClick);
    }

    public void ShowQuote(string quote)
    {
        _text.text = quote;
    }
}