using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Room
{
    public class SaveData : MonoBehaviour
    {
        public static string path;
        public List<SaveData> savedDataFiles;
        public GameData gameData;
        public GameObject chair;
        public GameObject plant;
        public GameObject whalPaper;
        public GameObject table;
        public GameObject painting;

        const string BLUE_CHEIR_KEY = "BlueChair";

        Chair changeChairData;
        Plant changePlantData;
        WhallPaper changeWhallPaperData;
        Table tableData;
        Painting paintingData;

        void Awake()
        {
            savedDataFiles = new List<SaveData>();

            path = Application.dataPath + "/Timerlan/Room/SaveData.json";
            gameData = new GameData();

            changeChairData = chair.GetComponent<Chair>();
            changePlantData = plant.GetComponent<Plant>();
            changeWhallPaperData = whalPaper.GetComponent<WhallPaper>();
            tableData = table.GetComponent<Table>();
            paintingData = painting.GetComponent<Painting>();
        }

        private void Start()
        {
            Load();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.F7))
            {
                Load();
            }
        }

        public void Load()
        {
            if (File.Exists(path))
            {
                string data = File.ReadAllText(path);

                gameData = JsonUtility.FromJson<GameData>(data);

                changeChairData.SetIndex(gameData.indexChair);
                changePlantData.SetIndex(gameData.indexPlant);
                changeWhallPaperData.SetIndex(gameData.indexWhallPaper);
                tableData.SetIndex(gameData.indexTable);
                paintingData.SetIndex(gameData.indexPainting);
            }
            else
                Debug.Log("Can`t read save data");
        }
        public void Save()
        {

            gameData.indexChair = changeChairData.i;
            gameData.indexPlant = changePlantData.i;
            gameData.indexWhallPaper = changeWhallPaperData.i;
            gameData.indexTable = tableData.i;
            gameData.indexPainting = paintingData.i;

            string jsonData = JsonUtility.ToJson(gameData, true);

            File.WriteAllText(path, jsonData);
            Debug.Log("The data saved");
        }
    }

    [System.Serializable]

    public class GameData
    {
        public int indexChair;
        public int indexPlant;
        public int indexWhallPaper;
        public int indexTable;
        public int indexPainting;
    }
}