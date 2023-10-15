using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Destruction
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        
        private int _score;
        public static Score Instance { get; private set; }

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if(Instance != this)
                Destroy(Instance);
        }

        public void AddScore()
        {
            _score++;
            scoreText.text = _score.ToString(); 
        }
    }
}