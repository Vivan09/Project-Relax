using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class CardController : MonoBehaviour
{
    [Header("Menu")]
    public TMP_Text helloText;
    public Button nextButton;
    public string name = "____";
    
    [Header("Cards")]
    [SerializeField] private MoodManager _moodsManager;
    [SerializeField] private Transform _emojiCardsParent;
    [SerializeField] private Transform _quoteCardsParent;
    [SerializeField] private GameObject _emojiCardPrefab;
    [SerializeField] private GameObject _quoteCardPrefab;
    
    private Dictionary<Moods, List<string>> _moodQuotes = new Dictionary<Moods, List<string>>();
    private ProfileData profileData = ProfileData.GetInstance();
    private List<GameObject> _quoteCards = new List<GameObject>();

    void Start()
    {
        ShowName();
        CreateCards();
        nextButton.onClick.AddListener(NextScene);
    }

    private void ShowName()
    {
        name = profileData.GetName();
        helloText.text = ($"Привіт, {name}!");
    }

    private void CreateCards()
    {
        _moodQuotes = _moodsManager.GetMoodsAndQuotes();
        
        foreach (var mood in _moodQuotes)
        {
            GameObject newCard = Instantiate(_emojiCardPrefab, _emojiCardsParent);
            CardItem cardItem = newCard.GetComponent<CardItem>();
            cardItem.ShowMood(mood.Key, _moodsManager.GetSpriteForMood(mood.Key));
            cardItem.SetClickEvent(() =>
            {
                SetMoodAndShowQuotes(mood.Key);
            });
        }
        _emojiCardsParent.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        ShowQuotesForCurrentMood(_moodQuotes.First().Key);
    }

    private void SetMoodAndShowQuotes(Moods mood)
    {
        _moodsManager.SetMoodByButton(mood);
        ShowQuotesForCurrentMood(mood);
    }

    private void ShowQuotesForCurrentMood(Moods mood)
    {
        _quoteCards.ForEach(go => Destroy(go));
        List<string> currentQuotes = _moodQuotes[mood];
        foreach (string quote in currentQuotes)
        {
            GameObject newCard = Instantiate(_quoteCardPrefab, _quoteCardsParent);
            newCard.GetComponent<CardQuoteItem>().ShowQuote(quote);
            _quoteCards.Add(newCard);
        }
        _quoteCardsParent.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    private void NextScene()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
