using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public string playerName;
    public string highScorePlayerName;
    public int highScore;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public string highScorePlayerName;
        public string playerName;
        public int highScore;
    }

    public void SavePlayerName()
    {
        SaveData data = new SaveData();
        data.playerName = playerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadPlayerName()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.playerName;
        }
    }

    public void SaveHighScore(string playerName, int highScore)
    {
        SaveData data = new SaveData();
        data.highScorePlayerName = playerName;
        data.highScore = highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/highscore.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScorePlayerName = data.highScorePlayerName;
            highScore = data.highScore;
        }
        else
        {
            highScorePlayerName = "";
            highScore = 0;
        }
    }
}
