using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using Application = UnityEngine.Application;

public static class GameCache
{
    public static GameCacheContainer cacheContainer { get; set; } = new GameCacheContainer();
    private static string pathSaveData;
    private static string folberName = "/cache/";
    private static string fileName = "profile.drs";

    private static string PASSWORD_DECRYPT = "<gSrIm[[6mjWOnRMp{:&Wi,3*";


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
        string encypt_json = StringCipher.Encrypt(json_str, PASSWORD_DECRYPT);
        File.WriteAllText(pathSaveData + fileName, encypt_json);
    }


    public static void ReadPlayerData()
    {
        IniPathSystem();
        if (File.Exists(pathSaveData + fileName))
        {
            string out_data = File.ReadAllText(pathSaveData + fileName);
            try
            {
cacheContainer = JsonConvert.DeserializeObject<GameCacheContainer>(StringCipher.Decrypt(out_data, PASSWORD_DECRYPT));
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

    public static bool FileSaveExits()
    {
        return File.Exists(pathSaveData + fileName);
    }

    public static bool ContainsZombieInBook (string zombieName)
    {
        return cacheContainer.ZombieBook.Contains(zombieName);
    }
}
