using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SwitcherManager : MonoBehaviour
{
    [SerializeField] private int startMenu = 0;
    [SerializeField] float duration = 0.4f;

    public infosScenes[] infosScenes;


    [ContextMenu("Загрузть Canvas Group")]
    private void loadCanvasGroupFromScenes()
    {
        foreach (var item in infosScenes)
        {
            item.canvasGroup = item.SceneMenu.GetComponent<CanvasGroup>();
        }
    }

    private void Start()
    {
        if (infosScenes.Length > 0)
        {
            SwitchScene(startMenu);
        }
    }

    public void SwitchScene(int indexScene)
    {
        ShowMenu(indexScene);
    }

    public void ShowMenu(int menuIndex)
    {
        HideCurrentMenu();

        infosScenes[menuIndex].canvasGroup.DOFade(1f, duration);
        infosScenes[menuIndex].canvasGroup.interactable = true;
        infosScenes[menuIndex].SceneMenu.SetActive(true);

        infosScenes[menuIndex].buttonImage.sprite = infosScenes[menuIndex].iconSelect;
    }

    private void HideCurrentMenu()
    {
        foreach (var item in infosScenes)
        {
            Debug.Log("Hide");

            item.canvasGroup.DOFade(0f, duration);
            item.canvasGroup.interactable = false;
            item.SceneMenu.SetActive(false);

            item.buttonImage.sprite = item.iconDefault;
        }
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}

[Serializable]
public class infosScenes
{
    public GameObject SceneMenu;
    public Image buttonImage;
    public Sprite iconDefault;  
    public Sprite iconSelect;  
    public CanvasGroup canvasGroup;
}
