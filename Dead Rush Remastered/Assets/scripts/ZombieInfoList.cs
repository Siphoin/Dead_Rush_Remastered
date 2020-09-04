using System;
using System.Collections.Generic;
using Newtonsoft.Json;
[Serializable]
  public  class ZombieInfoList
    {
    [JsonProperty("zombies")]
    public Dictionary<string, ZombieInfo> zombieList = new Dictionary<string, ZombieInfo>();
    }
