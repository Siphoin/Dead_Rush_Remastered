using System;
using UnityEngine;
using UnityEngine.UI;

public class NewZombieWindow : Window
{
    ZombieInfo zombieInfo = null;
    [SerializeField] Text text_description;
    [SerializeField] Text text_armor;
    [SerializeField] Text text_health;
    [SerializeField] Text text_damage;
    [SerializeField] Text text_name_zombie;
    [SerializeField] private GameObject window_anim;
    [SerializeField] Image zombie_skin_display;


    // Use this for initialization
    void Start()
    {
        target_window = window_anim;
        StartAnimation();
        if (zombieInfo == null)
        {
            throw new NullReferenceException("zombieinfo null");
        }
        SetChildEvent(WriteZombieToBook);
    }

    private void WriteZombieToBook()
    {
        GameCache.player_cacheContainer.ZombieBook.Add(zombieInfo.prefab_name);
        GameCache.SaveData();
    }

    // Update is called once per frame
    void Update()
    {
        AnimationProcess();
    }

    public void SetInfo(ZombieInfo info)
    {
        zombieInfo = info;
        text_damage.text = zombieInfo.damage.ToString();
        text_armor.text = zombieInfo.armor.ToString();
        text_health.text = zombieInfo.health.ToString();
        text_name_zombie.text = LanguageManager.Language == Language.EN ? zombieInfo.nameZombie_en_EN : zombieInfo.nameZombie_ru_RU;
        text_description.text = LanguageManager.Language == Language.EN ? zombieInfo.abilities_description_en_EN : zombieInfo.abilities_description_ru_RU;
        Sprite zombie_graphic_obj = Resources.Load<SpriteRenderer>($"Prefabs/Zombies/{ info.prefab_name}").sprite;
        zombie_skin_display.sprite = zombie_graphic_obj;
    }
}
