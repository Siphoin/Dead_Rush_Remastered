using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using Application = UnityEngine.Application;

public static class GameCache
{
    public static GameCacheContainer Player_cacheContainer { get; set; } = new GameCacheContainer();
    private static string pathSaveData;
    private static string folberName = "/cache/";
    private static readonly string fileName = "profile.json";



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
        string json_str = JsonConvert.SerializeObject(Player_cacheContainer, Formatting.Indented);
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
                Player_cacheContainer = JsonConvert.DeserializeObject<GameCacheContainer>(out_data);
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
        if (!Player_cacheContainer.levelsData.ContainsKey(data.index))
        {
            Player_cacheContainer.levelsData.Add(data.index, data);
        }

        else
        {
            if (data.starsCount > Player_cacheContainer.levelsData[data.index].starsCount)
            {
                Player_cacheContainer.levelsData[data.index] = data;
            }


        }

        if (Player_cacheContainer.levelCompleted == data.index)
        {
            Player_cacheContainer.levelCompleted++;
        }

        SaveData();


    }

    public static bool FileSaveExits()
    {
        return File.Exists(pathSaveData + fileName);
    }

    public static bool ContainsZombieInBook(string zombieName)
    {
        return Player_cacheContainer.ZombieBook.Contains(zombieName);
    }


    public static bool GameFinished()
    {
        return Player_cacheContainer.levelCompleted >= LevelManager.MAX_LEVEL_GAME;
    }

    public static void DeleteSave()
    {
        if (FileSaveExits())
        {
            File.Delete(pathSaveData + fileName);
        }
    }

}
