using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadPlayerAndScore();
    }

    public string topPlayerName;
    public string curPlayerName;
    public int highestScore;

    [System.Serializable]
    class SaveData
    {
        public string topPlayerName;
        public int highestScore;
    }

    public void SavePlayerAndScore()
    {
        SaveData saveData = new SaveData();
        saveData.topPlayerName = topPlayerName;
        saveData.highestScore = highestScore;
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "savefile.json", json);
    }

    public void LoadPlayerAndScore()
    {
        string path = Application.persistentDataPath + "savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            topPlayerName = data.topPlayerName;
            highestScore = data.highestScore;
        }
    }

}
