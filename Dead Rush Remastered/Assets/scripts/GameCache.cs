using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using Application = UnityEngine.Application;

public static class GameCache
{
    public static GameCacheContainer player_cacheContainer { get; set; } = new GameCacheContainer();
    private static string pathSaveData;
    private static string folberName = "/cache/";
    private static string fileName = "profile.json";



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
        string json_str = JsonConvert.SerializeObject(player_cacheContainer, Formatting.Indented);
        File.WriteAllText(pathSaveData + fileName, json_str);
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    public static void GetData()
    {
        IniPathSystem();
        if (File.Exists(pathSaveData + fileName))
        {
            string out_data = File.ReadAllText(pathSaveData + fileName);
            try
            {
                player_cacheContainer = JsonConvert.DeserializeObject<GameCacheContainer>(out_data);
            }

            catch (JsonException e)
            {
                Debug.LogError($"JsonExpection: {e.Message}");
                Application.Quit();

            }
            
        }
    }

    public static void SetProgressLevel(LevelIProgressData data)
    {
        IniPathSystem();
        if (!player_cacheContainer.levelsData.ContainsKey(data.index))
        {
            player_cacheContainer.levelsData.Add(data.index, data);
        }

        else
        {
            if (data.starsCount > player_cacheContainer.levelsData[data.index].starsCount)
            {
                player_cacheContainer.levelsData[data.index] = data;
            }


        }

        if (player_cacheContainer.levelCompleted == data.index)
        {
            player_cacheContainer.levelCompleted++;
        }

        SaveData();


    }

    public static bool FileSaveExits()
    {
        return File.Exists(pathSaveData + fileName);
    }

    public static bool ContainsZombieInBook (string zombieName)
    {
        return player_cacheContainer.ZombieBook.Contains(zombieName);
    }

}
