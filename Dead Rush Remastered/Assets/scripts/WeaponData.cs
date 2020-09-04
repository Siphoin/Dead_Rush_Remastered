using Newtonsoft.Json;
using System;
[Serializable]
   public class WeaponData
    {
    [JsonProperty("max_ammunition")]
  public  int maxAmmunition = 40;

    [JsonProperty("name")]

    public string name_weapon = "gun";

    [JsonProperty("reload")]
    public float reloadTime = 2;
}
