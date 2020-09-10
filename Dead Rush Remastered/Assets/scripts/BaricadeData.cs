using Newtonsoft.Json;
using System;
[Serializable]
public class BaricadeData
{
    [JsonProperty("name")]
    public string name_prefab = "baricades1";
    public int sale = 0;
    public int armor = 0;
    public int health = 0;
}


