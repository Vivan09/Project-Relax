using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TutorialManager : MonoBehaviour
{
    public bool hasCompletedTheTutorial = false;

    public List<GameObject> tutorialTexts;

    public Building buidlingScript;
    public static string path;

    private bool check = false;

    private TutorialData tutorialData;

    void Update()
    {
        if(buidlingScript._canPlaceTile && check)
        {
           DeleteTutorialText();
           check = false;
        }
    }

    void Awake()
    {
        tutorialData = new TutorialData();
        path = Application.dataPath + "/tutorialData.json";
        Load();
        if(hasCompletedTheTutorial)
        {
            foreach(var text in tutorialTexts)
            {
                Destroy(text);
            }
        }
    }

    public void DeleteTutorialText()
    {
       if(tutorialTexts.Count != 0)
       {
           int id = tutorialTexts.Count - 1;
           Destroy(tutorialTexts[id]);
           tutorialTexts.Remove(tutorialTexts[id]);
           if(id != 0)
           {
               tutorialTexts[id - 1].SetActive(true);
           } else if(tutorialTexts.Count == 0)
           {
               this.gameObject.GetComponent<TutorialManager>().enabled = false;
               Save();
           }
       }
    }

    void Save()
    {
        tutorialData.hasCompletedTheTutorial = true;
        string json = JsonUtility.ToJson(tutorialData, true);
        File.WriteAllText(path, json);
    }

    void Load()
    {
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            TutorialData data = JsonUtility.FromJson<TutorialData>(json);
            hasCompletedTheTutorial = data.hasCompletedTheTutorial;
        }
    }

    void OnClick()
    {
        check = true;
    }

}

public class TutorialData
{
    public bool hasCompletedTheTutorial = false;
}