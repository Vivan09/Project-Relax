using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DataManagement
{

    public class DataManagementSystem<T> : MonoBehaviour
    {
        private DateTime _startTime;
        private DateTime _endTime;
        
        private static DataManagementSystem<T> _instance;

        public static DataManagementSystem<T> Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<DataManagementSystem<T>>();

                    if (_instance == null) _instance = new GameObject().AddComponent<DataManagementSystem<T>>();

                    DontDestroyOnLoad(_instance);
                }

                return _instance;
            }
        }

        void Awake()
        {
            if (_instance == null) _instance = this;
            else Destroy(this);
            
            DontDestroyOnLoad(this);
        }

        public virtual void RegisterStartData(T startData)
        {
            _startTime = DateTime.Now;
        }
                
        public virtual void RegisterEndData(T endData)
        {
            _endTime = DateTime.Now;
        }

        private void CalculateTimeSpent()
        {
            TimeSpan timeDifference = _endTime - _startTime;

            int hours = timeDifference.Hours;
            int minutes = timeDifference.Minutes;
        }

        public void SaveData<TP>(TP data, string fileName) where TP : GameData
        {
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            string jsonData = JsonUtility.ToJson(data);
            File.WriteAllText(filePath, jsonData);
        }

        public GameData LoadData(string fileName)
        {
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            if (File.Exists(filePath))
            {
                string loadedData = File.ReadAllText(filePath);
                return JsonUtility.FromJson<GameData>(loadedData);
            }

            return null;
        }
    }

    public abstract class GameData
    {
        public abstract void Reset();
    }
}