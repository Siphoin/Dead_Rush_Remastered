using Newtonsoft.Json;
using System;
[Serializable]
public class WeaponData
{
    [JsonProperty("max_ammunition")]
    public int maxAmmunition = 25;

    [JsonProperty("name")]

    public string name_weapon = "gun";

    [JsonProperty("reload")]
    public float reloadTime = 2;

    public int sale = 100;
    [UnityEngine.HideInInspector]
    public int level_upgrate = 1;
}
