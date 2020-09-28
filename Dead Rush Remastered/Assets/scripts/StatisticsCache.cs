using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public static   class StatisticsCache
    {
    public static UserStatistics Statistics { get; set; } = new UserStatistics();
    private static string pathSaveData;
    private static string folberName = "/cache/";
    private static readonly string fileName = "statistics.json";

    private static void IniPathSystem()
    {
        if (!Application.isEditor)
        {
            pathSaveData = Application.persistentDataPath + folberName;
        }

        else
        {
            pathSaveData = Application.dataPath + folberName;
        }
    }

    public static void SaveData()
    {
        IniPathSystem();
        if (!Directory.Exists(pathSaveData))
        {
            Directory.CreateDirectory(pathSaveData);
        }
        string json_str = JsonConvert.SerializeObject(Statistics);
        File.WriteAllText(pathSaveData + fileName, json_str);
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    public static void GetData()
    {
        IniPathSystem();
        if (FileSaveExits())
        {
            string out_data = File.ReadAllText(pathSaveData + fileName);
            try
            {
                Statistics = JsonConvert.DeserializeObject<UserStatistics>(out_data);
            }

            catch (JsonException e)
            {
                Debug.LogError($"JsonExpection: {e.Message}");
                Application.Quit();

            }

        }
    }

  

    public static bool FileSaveExits()
    {
        return File.Exists(pathSaveData + fileName);
    }
}
