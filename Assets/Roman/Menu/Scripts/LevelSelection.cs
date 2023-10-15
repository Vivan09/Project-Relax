using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button homeButton;
    public Button scene1Button;
    public Button scene2Button;
    public Button scene3Button;

    void Start()
    {
        homeButton.onClick.AddListener(OnHomeClick);
        scene1Button.onClick.AddListener(OnScene1Click);
        scene2Button.onClick.AddListener(OnScene2Click);
        scene3Button.onClick.AddListener(OnScene3Click);
    }

    void OnHomeClick()
    {
        SceneManager.LoadScene(1);
    }

    void OnScene1Click()
    {
        SceneManager.LoadScene("Building Game");
    }

    void OnScene2Click()
    {
        SceneManager.LoadScene("BuildingGameScene2");
    }

    void OnScene3Click()
    {
        SceneManager.LoadScene("BuildingGameScene3");
    }
}
