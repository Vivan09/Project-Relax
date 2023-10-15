using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quotes : MonoBehaviour
{
    [SerializeField] private TMP_Text QuotesText;
    [SerializeField] private List<string> QuotesList = new List<string>();

    private void Start()
    {
        RandomQuotes();
    }

    public void RandomQuotes()
    {
        int rQuote = Random.Range(0, QuotesList.Count);
        QuotesText.text = QuotesList[rQuote];
    }
}
