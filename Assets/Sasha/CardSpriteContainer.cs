using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "CardSpriteContainer", menuName = "ScriptableObjects/CardSpriteContainer")]
public class CardSpriteContainer : ScriptableObject
{
    [FormerlySerializedAs("spriteItems")] public List<CardSprite> CardSprites = new List<CardSprite>();

    public Sprite GetSprite(Moods mood)
    {
        CardSprite cardSprite = CardSprites.Find(s => s.Mood == mood);
        return cardSprite.Sprite;
    }
}

[System.Serializable]
public class CardSprite
{
    public Moods Mood;
    public Sprite Sprite;
}
