using System.Collections.Generic;
using UnityEngine;

public interface IMoodManager
{
    public void SetMoodQuote();
    public void SaveMoodData();
    public void LoadMoodData();
    public void SaveReason();
    public Dictionary<Moods, List<string>> GetMoodsAndQuotes();
    public Sprite GetSpriteForMood(Moods mood);
}
