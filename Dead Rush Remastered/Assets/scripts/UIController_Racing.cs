﻿using UnityEngine;
using UnityEngine.UI;

public class UIController_Racing : MonoBehaviour
{
    [SerializeField] TransliteText text_kill_count_zombies;
    [SerializeField] TransliteText text_kills;
    [SerializeField] Text text_timer;
    [SerializeField] Slider slider_hp;
    // Use this for initialization
    void Start()
    {
        ShowCountKillsZombies();
        LevelManager_Racing.manager.zombieKillEvent += ShowCountKillsZombies;
        slider_hp.maxValue = LevelManager_Racing.manager.Car.Health;
        LevelManager_Racing.manager.OnEndLevel += Hide;
    }

    private void Hide()
    {
        Destroy(gameObject);
    }

    private void ShowCountKillsZombies()
    {
        string str_translite_kills = $" {LevelManager_Racing.manager.ZombieKillCount}";
        text_kill_count_zombies.SetupText(str_translite_kills, str_translite_kills);
        string str_translite_left_zombies = $" {LevelManager_Racing.manager.ZombiesMax - LevelManager_Racing.manager.ZombieKillCount}";
        text_kills.SetupText(str_translite_left_zombies, str_translite_left_zombies);
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager_Racing.manager.Car != null)
        {
            slider_hp.value = LevelManager_Racing.manager.Car.Health;
        }

        text_timer.text = LevelManager_Racing.manager.timerString;
    }


}
