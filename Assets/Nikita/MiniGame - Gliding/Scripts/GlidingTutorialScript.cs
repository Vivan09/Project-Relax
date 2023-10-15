using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlidingTutorialScript : MonoBehaviour
{
    [SerializeField] private GameObject tutorial;
    void Start()
    {
        if (!PlayerPrefs.HasKey("TutorialShown"))
        {
            ShowTutorial();
        }
    }

    private void ShowTutorial()
    {
        tutorial.SetActive(true);

        PlayerPrefs.SetInt("TutorialShown", 1);
        PlayerPrefs.Save();
    }
}
