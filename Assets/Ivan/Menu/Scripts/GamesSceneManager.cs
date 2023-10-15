using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Ivan.Menu.Scripts
{
    public class GamesSceneManager : MonoBehaviour
    {
        public Image perehodpanel;
        public Transform parentObj;
        public GameObject prefabButton;
        public List<GameInfo> gameInfos;

        void Start()
        {
            foreach (var button in gameInfos)
            {
                CreateButton(button.nameGame, button.descriptionScene, button.icoGame, button.nameScene);
            }
        }

        void CreateButton(string nameGame, string descriptionGame, Sprite icon, string nameScene)
        {
            GameObject obj = Instantiate(prefabButton, transform.position, Quaternion.identity);
            obj.transform.SetParent(parentObj);

            var buttonInfo = obj.GetComponent<SceneButton>();
            buttonInfo.Refresh(nameGame, descriptionGame, icon);

            var button = obj.GetComponent<Button>();
            button.onClick.AddListener(() => LoadScene(nameScene));
        }

        void LoadScene(string nameScene)
        {
            SceneManager.LoadScene(nameScene);
        }
    }

    [System.Serializable]
    public class GameInfo
    {
        public string nameGame;
        public string nameScene;
        public string descriptionScene;
        public Sprite icoGame;
    }
}