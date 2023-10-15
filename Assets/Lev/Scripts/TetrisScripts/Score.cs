using System;
using SoftTetris;
using TMPro;
using UnityEngine;

namespace Tetris
{
    public class Score : MonoBehaviour
    {
        public TMP_Text scoreText;

        private int _level = 1;
        private int _score = 0;
        private int _clearedLinesCounter = 0;

        private readonly int[] _scoreCount = {40, 100, 300, 1200};

        public int MaxScore { get; private set; }

        private void OnEnable() => FindObjectOfType<UIManager>().OnGameOver.AddListener(UpdateMaxScore);

        public void UpdateScore(int clearedLines)
        {
            if (clearedLines == 0)
                return;

            _score += _scoreCount[clearedLines - 1] * _level;

            _clearedLinesCounter += clearedLines;
            if (_clearedLinesCounter >= 10)
            {
                _clearedLinesCounter = _clearedLinesCounter % 10;
                _level++;
            }

            scoreText.text = "Score: " + _score;
        }

        private void UpdateMaxScore()
        {
            if (_score > MaxScore) MaxScore = _score;
        }
    }
}

