﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class ResultLevelWindow : AnimatedWindow
{
    [SerializeField] Text text_state_level;
    private Color color_text_victory = new Color32(100, 202, 120, 255);
    private Color color_text_defeat = new Color32(202, 100, 102, 255);
    [SerializeField] Image[] starsImages;
    private Sprite star_yes_result_sprite;
    private string levelNextName;
    [SerializeField] Text text_result_level;

    [SerializeField] Button button_next_level;
    [SerializeField] GameObject window_anim;
    [SerializeField] Button button_back_menu;

    private string dialogName = null;

    // Use this for initialization
    void Start()
    {
        target_window = window_anim;
        StartAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        AnimationProcess();

    }

    private void SetTimeScale(int value)
    {
        Time.timeScale = value;
    }

    public void SetState(ResultLevel result, int starsCount, int moneyCost, LevelIProgressData data, LevelStats levelStats)
    {
        star_yes_result_sprite = Resources.Load<Sprite>("UI/star_level_on_result");
        if (starsCount > 3 || starsCount < 0)
        {
            throw new ArgumentException("Invalid value of the starsCount argument. Acceptable values are up to 0 and 3.");
        }
        string text_state = result.ToString().ToUpper();
        if (text_state == "DEFEAT")
        {
           if (LanguageManager.Language == Language.RU)
            {
                text_state = "ПОРАЖЕНИЕ";
            }
        }

        if (text_state == "VICTORY")
        {
            if (LanguageManager.Language == Language.RU)
            {
                text_state = "ПОБЕДА";
            }
        }
        text_state_level.text = text_state;
        if (result == ResultLevel.Defeat)
        {
            Destroy(button_next_level.gameObject);
            text_state_level.color = color_text_defeat;
            GameCache.SaveData();
        }
        if (result == ResultLevel.Victory)
        {
            GameCache.SetProgressLevel(data);
            text_state_level.color = color_text_victory;
            levelNextName = $"Level{data.index + 1}";
        }

        for (int i = 0; i < starsCount; i++)
        {
            starsImages[i].sprite = star_yes_result_sprite;
        }

        GameCache.Player_cacheContainer.money += moneyCost;
        if (LanguageManager.Language == Language.EN)
        {
            text_result_level.text = $"Zombies killed: {CurrencyRounder.Round(levelStats.zombiesKilled)}\nMoney: {CurrencyRounder.Round(levelStats.moneyCost)} $\nTime: {levelStats.timeLevelCompleting.ToLongTimeString()}";
        }

        else
        {
            text_result_level.text = $"Зомби убито: {CurrencyRounder.Round(levelStats.zombiesKilled)}\nДенег заработано: {CurrencyRounder.Round(levelStats.moneyCost)} $\nВремя: {levelStats.timeLevelCompleting.ToLongTimeString()}";
        }


        }

        public void ExitToMenu()
    {
        SetTimeScale(1);
        Menu.OnPageLevels = true;
        Loading.OnLoad("Menu");
    }

    public void SetSceneDialogName (string name)
    {
        if (string.IsNullOrEmpty(name)) {
            throw new NullReferenceException("name scene dialog is null!");
        }

        dialogName = name;


    }

    public void NextLevel()
    {
        
        SetTimeScale(1);
        switch (dialogName)
        {
            case null:
                Loading.OnLoad(levelNextName);
                break;
            default:
                Loading.OnLoad(dialogName);
                break;
        }

    }




    }