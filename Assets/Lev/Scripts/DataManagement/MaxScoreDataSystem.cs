using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataManagement;
using UnityEngine;

namespace DataManagement
{

    public sealed class MaxScoreDataSystem : DataManagementSystem<int>
    {
        [SerializeField] private string fileName;
        
        private readonly List<int> _scores = new List<int>();

        public void RegisterIntermediateValues(int score)
        {
            _scores.Add(score);
        }

        public override void RegisterEndData(int endData)
        {
            base.RegisterEndData(endData);
            _scores.Add(endData);
            
        }

        private double CalculateAverageScore() => _scores.Average();
    }

    public sealed class ScoreData : GameData
    {
        public double Average;
        public int MaxScore;

        public ScoreData(double average, int maxScore)
        {
            Average = average;
            MaxScore = maxScore;
        }

        public override void Reset()
        {
            Average = 0;
            MaxScore = 0;
        }
    }
}
