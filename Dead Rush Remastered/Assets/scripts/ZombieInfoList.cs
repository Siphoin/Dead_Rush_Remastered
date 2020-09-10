using Newtonsoft.Json;
using System;
using System.Collections.Generic;
[Serializable]
public class ZombieInfoList
{
    [JsonProperty("zombies")]
    public Dictionary<string, ZombieInfo> zombieList = new Dictionary<string, ZombieInfo>();
}
