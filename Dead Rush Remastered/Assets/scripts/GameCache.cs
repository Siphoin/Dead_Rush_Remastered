using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Application = UnityEngine.Application;

public static class GameCache
{
    public static GameCacheContainer cacheContainer { get; set; } = new GameCacheContainer();
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

    public static void WritePlayerCache()
    {
        IniPathSystem();
        if (!Directory.Exists(pathSaveData))
        {
            Directory.CreateDirectory(pathSaveData);
        }
        string json_str = JsonConvert.SerializeObject(cacheContainer, Formatting.Indented);
        File.WriteAllText(pathSaveData + fileName, json_str);
    }


    public static void ReadPlayerData()
    {
        IniPathSystem();
        if (File.Exists(pathSaveData + fileName))
        {
            string out_data = File.ReadAllText(pathSaveData + fileName);
            cacheContainer = JsonConvert.DeserializeObject<GameCacheContainer>(out_data);
        }
    }

    public static void SetProgressLevel(LevelIProgressData data)
    {
        IniPathSystem();
        if (!cacheContainer.levelsData.ContainsKey(data.index))
        {
            cacheContainer.levelsData.Add(data.index, data);
        }

        else
        {
            if (data.starsCount > cacheContainer.levelsData[data.index].starsCount)
            {
 cacheContainer.levelsData[data.index] = data;
            }
           

        }

        if (cacheContainer.levelCompleted == data.index)
        {
            cacheContainer.levelCompleted++;
        }

        WritePlayerCache();


    }

    public static bool FileSaveExits ()
    {
        return File.Exists(pathSaveData + fileName);
    }
}
