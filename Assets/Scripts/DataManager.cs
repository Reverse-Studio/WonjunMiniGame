using System.Collections.Generic;
using UnityEngine;

// 게임 데이터 저장 매니저
public class DataManager : MonoBehaviour
{

    public static DataManager Instance
    {
        get => GetInstance();
    }

    private static DataManager instance;

    private static DataManager GetInstance()
    {
        if (instance == null)
        {
            var obj = new GameObject("DataManager");
            instance = obj.AddComponent<DataManager>();
            DontDestroyOnLoad(obj);
        }
        return instance;
    }

    #region UnityEvents

    private void Awake()
    {
        Load();
        Application.quitting += Save;
    }

    #endregion

    public enum Game
    {
        Tetris,  // 테트리스
        Bound,   // 공튀기기
        Count
    }

    public class Data
    {
        public int MaxScore;
    }

    readonly Dictionary<Game, Data> dataDict = new();

    // Load From PlayerPrefs
    public void Load()
    {
        for (int i = 0; i < (int)Game.Count; i++)
        {
            Game key = (Game)i;
            dataDict[key] = new Data()
            {
                MaxScore = PlayerPrefs.GetInt(key.ToString(), 0)
            };
        }
        Debug.Log("Data Load Compleate");
    }

    // Save To PlayerPref
    public void Save()
    {
        for (int i = 0; i < (int)Game.Count; i++)
        {
            Game key = (Game)i;
            if (dataDict.TryGetValue(key, out var data))
            {
                PlayerPrefs.SetInt(key.ToString(), data.MaxScore);
            }
        }
        PlayerPrefs.Save();
    }

    public int GetMaxScore(Game game)
    {
        return dataDict.TryGetValue(game, out var data) ? data.MaxScore : 0;
    }

    public void SetMaxScore(Game game, int maxScore)
    {
        if (!dataDict.TryGetValue(game, out var data))
        {
            data = new Data();
        }
        data.MaxScore = maxScore;
    }
}