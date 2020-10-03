using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public static class AudioCache
{
    public static AudioCacheData data { get; set; } = new AudioCacheData();
    private static string pathSaveData;
    private static string folberName = "/cache/";
    private static string fileName = "audio.json";



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
        string json_str = JsonConvert.SerializeObject(data);
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
                data = JsonConvert.DeserializeObject<AudioCacheData>(out_data);
            }

            catch (JsonException e)
            {
                Debug.LogError($"JsonExpection: {e.Message}");
                Application.Quit();

            }

        }
    }
}
