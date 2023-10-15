using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneButton : MonoBehaviour
{
    [SerializeField] private TMP_Text NameText;
    [SerializeField] private TMP_Text DescriptionText;
    [SerializeField] private Image IconImage;

    public void Refresh(string name, string description, Sprite icon)
    {
        NameText.text = name; 
        DescriptionText.text = description; 
        IconImage.sprite = icon;
    }
}
