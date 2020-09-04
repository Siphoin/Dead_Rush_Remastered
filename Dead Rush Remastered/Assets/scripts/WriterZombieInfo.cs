using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using Newtonsoft.Json;
using Debug = UnityEngine.Debug;
using System.IO;

public   class WriterZombieInfo
    {
    const string path = "Assets/Resources/manifests/zombies_info.json";
#if UNITY_EDITOR
    [MenuItem("Window/Dead Rush Tools/Write zombies info")]
    static void WriteInfoZombies ()
    {
        
        Debug.Log("WriterZombieInfo message: Write...");
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        ZombieBase[] zombiesList = Resources.LoadAll<ZombieBase>("Prefabs/Zombies");
        Debug.Log($"Found {zombiesList.Length} zombies in folber. Staring writing...");
        ZombieInfoList infoList = new ZombieInfoList();
        foreach (ZombieBase zombie in zombiesList)
        {           
            infoList.zombieList.Add(zombie.gameObject.name, zombie.GetInfo());
        }

        string json = JsonConvert.SerializeObject(infoList, Formatting.Indented);
        File.WriteAllText(path, json);
        stopwatch.Stop();
        Debug.Log($"File info is created. Path: {path} Time: {stopwatch.ElapsedMilliseconds} ms.");
    }
#endif
}
