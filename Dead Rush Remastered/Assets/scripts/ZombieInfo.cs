using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
[Serializable]
 public   class ZombieInfo
    {
    [JsonProperty("name_en-EN")]
    public string nameZombie_en_EN = "";
    [JsonProperty("name_ru-RU")]
    public string nameZombie_ru_RU = "";
    public int damage = 0;
    public int armor = 0;
    public int health = 0;
    [JsonProperty("type_attack")]
    public TypeAttackZombie typeAttackZombie = TypeAttackZombie.Near;
    [JsonProperty("description (en-EN)")]
    public string abilities_description_en_EN = "";
    [JsonProperty("description (ru-RU)")]
    public string abilities_description_ru_RU = "";
    [JsonProperty("prefab")]
    public string prefab_name = "";

    public ZombieInfo(int ARMOR, int HEALTH, int DAMAGE, TypeAttackZombie typeAttack, string prefabName)
    {
        armor = ARMOR;
        health = HEALTH;
        damage = DAMAGE;
        typeAttackZombie = typeAttack;
        prefab_name = prefabName;
    }
}
