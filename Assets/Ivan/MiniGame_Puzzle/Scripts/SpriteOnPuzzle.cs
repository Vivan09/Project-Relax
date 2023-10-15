using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOnPuzzle : MonoBehaviour
{
    private ManagerPuzzle managerPuzzle;

    private SpriteRenderer spriteRenderer;

    private void Start() {
        managerPuzzle = FindObjectOfType<ManagerPuzzle>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        managerPuzzle.PuzzleEvent += ChangeSprite;
    }

    private void ChangeSprite(Sprite newSprite){
        spriteRenderer.sprite = newSprite;
    }
}
