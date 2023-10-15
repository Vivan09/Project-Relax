using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ManagerPuzzle : MonoBehaviour
{
    public delegate void SpriteDelegat(Sprite spritePuzzles);
    public event SpriteDelegat PuzzleEvent;
    
    public delegate void NewPositionPuzzlesDelegat();

    public NewPositionPuzzlesDelegat NewPos;

    [SerializeField] private GameObject Menu;

    [SerializeField] private GameObject[] PuzzlesLevel; // уровни сложности
    private GameObject PuzzleLevelSelect; // обраний рівень складності

    private void Awake() {
        PuzzleLevelSelect = PuzzlesLevel[1];

        offPuzzleGame();
        PuzzleLevelSelect.SetActive(true);
    }

    public void InvokeChangeSprite(Sprite spritePuzzles){ // Кнопка вибору картинки пазлів
        if(PuzzleEvent != null)
        {
            PuzzleEvent.Invoke(spritePuzzles);
        }

        NewPos();
        
        PuzzleLevelSelect.SetActive(true);
        Menu.SetActive(false);
    }

    public void PuzzleLevel(GameObject puzzleLevel){
        PuzzleLevelSelect = puzzleLevel;
        NewPos();
    }

    public void ActiveMenu(bool active){
        Menu.SetActive(active);
        PuzzleLevelSelect.SetActive(!active);
    }

    private void offPuzzleGame(){
        foreach(var level in PuzzlesLevel){
            level.SetActive(false);
        }
    }
}