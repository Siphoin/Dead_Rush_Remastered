using Newtonsoft.Json;
using System;
using System.Collections.Generic;
[Serializable]
public class ZombieInfoList : IDisposable
{
    [JsonProperty("zombies")]
    public Dictionary<string, ZombieInfo> zombieList = new Dictionary<string, ZombieInfo>();

    public void Dispose()
    {
        zombieList.Clear();
    }
}
