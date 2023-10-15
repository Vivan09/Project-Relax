using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class CardItem : MonoBehaviour
{
    [SerializeField] private Image _emojiImage;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _button;

    private Moods _mood;

    public void SetClickEvent(UnityAction onClick)
    {
        _button.onClick.AddListener(onClick);
    }

    public void ShowMood(Moods mood, Sprite emojiImage)
    {
        _emojiImage.sprite = emojiImage;
        _text.text = mood.ToString();
        _mood = mood;
    }
}