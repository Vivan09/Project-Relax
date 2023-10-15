using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManagerUI : MonoBehaviour
{
    [SerializeField] private Spawn Spawn;

    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private GameObject MenuSettings;

    public int score;

    private void Start() {
        AddScore(0);
    }

    public void AddScore(int point){
        score += point;
        score = Mathf.Clamp(score, 0, score);
        ScoreText.text = score.ToString();
    }

    public void Settings(bool Open){
        MenuSettings.SetActive(Open);
    }
}
