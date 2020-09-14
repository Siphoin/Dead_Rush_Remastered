﻿using Newtonsoft.Json;
using System.Collections.Generic;

public class GameCacheContainer
{
    [JsonProperty("levels")]
    public Dictionary<int, LevelIProgressData> levelsData = new Dictionary<int, LevelIProgressData>();

    public string skin = "soldier1";

    [JsonProperty("level_num_end")]
    public int levelCompleted = 1;

    [JsonProperty("$")]
    public long money = 0;

    [JsonProperty("name_profile")]

    public string namePlayer = null;


    public BaricadeData baricades = new BaricadeData();

    [JsonProperty("weapon_selected")]

    public WeaponData selectedWeapon = new WeaponData();

    [JsonProperty("weapon_list_player")]

    public Dictionary<string, WeaponData> weaponsPlayer = new Dictionary<string, WeaponData>()
    {
        {"gun", new WeaponData() }
    };
    [JsonProperty("baricades_list")]
    public Dictionary<string, BaricadeData> baricadesPlayer = new Dictionary<string, BaricadeData>()
    {
        {"baricades1", new BaricadeData() }
    };

    [JsonProperty("partner")]
    public bool partnerBuyed = false;

    [JsonProperty("background")]

    public bool backgroundCompleted = false;

    [JsonProperty("zombies_book")]

    public HashSet<string> ZombieBook = new HashSet<string>();
}
