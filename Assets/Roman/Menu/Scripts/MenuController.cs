using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    public Button cardButton;
    public Button homeButton;
    public Button roomButton;
    public Button gameButton;
    public Button profileButton;

    public GameObject gameselection;
    public GameObject home;
    //public GameObject profile;

    public Sprite homeSprtOn;
    public Sprite homeSprtOff;
    public Sprite gameSprtOn;
    public Sprite gameSprtOff;

    RectTransform homeRect;
    RectTransform gameRect;
    void Start()
    {
        gameButton.onClick.AddListener(OnG);
        homeButton.onClick.AddListener(OnH);
        roomButton.onClick.AddListener(OnR);
        //profileButton.onClick.AddListener(OnS);
        cardButton.onClick.AddListener(OnC);

        homeRect = homeButton.GetComponent<RectTransform>();
        gameRect = gameButton.GetComponent<RectTransform>();
    }

    void OnH()
    {
        gameselection.SetActive(false);
        home.SetActive(true);
        //profile.SetActive(false);

        homeButton.image.sprite = homeSprtOn;
        homeRect.sizeDelta = new Vector2(91, 135);

        gameButton.image.sprite = gameSprtOff;
        gameRect.sizeDelta = new Vector2(123, 76);
    }

    void OnR()
    {
        SceneManager.LoadScene("Room");
    }

    void OnG()
    {
        gameselection.SetActive(true);
        home.SetActive(false);
        //profile.SetActive(false);

        homeButton.image.sprite = homeSprtOff;
        homeRect.sizeDelta = new Vector2(92, 101);

        gameButton.image.sprite = gameSprtOn;
        gameRect.sizeDelta = new Vector2(122, 108);
    }

    void OnS()
    {
        gameselection.SetActive(false);
        home.SetActive(false);
        //profile.SetActive(true);
    }

    void OnC()
    {
        SceneManager.LoadScene("Menu");
    }
}
