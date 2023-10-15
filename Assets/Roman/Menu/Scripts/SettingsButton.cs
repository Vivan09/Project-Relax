using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    //private bool isPaused = false;

    public GameObject settingsMenu;
    public GameObject settingsButton;

    void Start()
    {
        Time.timeScale = 1f;
    }

    public void OnSettingsButtonClick()
    {
        Time.timeScale = 0f;
        //isPaused = true;
        settingsMenu.SetActive(true);
        settingsButton.SetActive(false);
        //if (isPaused)
        //    ResumeGame();
        //else
        //    PauseGame();
    }

    public void OnContinueButtonClick()
    {
        Time.timeScale = 1f;
        //isPaused = false;
        settingsButton.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void OnHomeButtonClick()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void OnResetButtonClick()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //void PauseGame()
    //{
    //    Time.timeScale = 0f; 
    //    isPaused = true;
    //    settingsMenu.SetActive(true);
    //}

    //void ResumeGame()
    //{
    //    Time.timeScale = 1f;
    //    isPaused = false;
    //    settingsMenu.SetActive(false);
    //}
}
