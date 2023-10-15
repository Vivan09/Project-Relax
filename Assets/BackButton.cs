using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public void BackOnMenu(string nameScene){
        SceneManager.LoadScene(nameScene);
    }
}
