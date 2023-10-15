using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using DG.Tweening;

public class ProfileConfig : MonoBehaviour
{
    [SerializeField] private ManageProfile manageProfile;

    [Header("Data")]
    [SerializeField] private Sprite spriteBgAva;
    [SerializeField] private Color colorAva;
    [SerializeField] public int NumberColor; // Для збереження
    
    [Header("Компоненти")]
    [SerializeField] private Image imageBgAva;
    [SerializeField] private Image imageAva;
    [SerializeField] private Image ramkaButton;
    
    private Image _imageBgButton; // имедж кнопки на яку натискаю при виборі беграунду для аватарки
    private Button _buttonColor; // компонет кнопки

    private void Start()
    {
        _imageBgButton = GetComponent<Image>();
        _buttonColor = GetComponent<Button>();
        
        _imageBgButton.sprite = spriteBgAva;
        _buttonColor.onClick.AddListener(ColorBgAvaUpdate);

        var objsColors = FindObjectsOfType<ProfileConfig>();
        int colorN = manageProfile.GetColor();

        foreach (var item in objsColors)
        {
            if(item.NumberColor == colorN)
            {
                item.ColorBgAvaUpdate();
            }
        }
    }

    public void ColorBgAvaUpdate()
    {
        imageBgAva.sprite = spriteBgAva;
        imageAva.color = colorAva;

        manageProfile.ChangeColor(NumberColor); // збереження
        
        AnimRamka(transform);
    }

    private void AnimRamka(Transform target)
    {
        float posY = transform.position.y;

        ramkaButton.transform.DOMoveX(target.position.x, 1f);
    }
}
