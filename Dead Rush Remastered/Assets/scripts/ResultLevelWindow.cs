using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

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

    private  void SetTimeScale(int value)
    {
        Time.timeScale = value;
    }

    public void SetState (ResultLevel result, int starsCount, int moneyCost, LevelIProgressData data, LevelStats levelStats)
    {
        star_yes_result_sprite = Resources.Load<Sprite>("UI/star_level_on_result");
        if (starsCount > 3 || starsCount < 0)
        {
            throw new ArgumentException("Invalid value of the starsCount argument. Acceptable values are up to 0 and 3.");
        }
        string text_state = result.ToString().ToUpper();
        text_state_level.text = text_state;
        if (result == ResultLevel.Defeat)
        {
            Destroy(button_next_level.gameObject);
            text_state_level.color = color_text_defeat;
            GameCache.WritePlayerCache();
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

        GameCache.cacheContainer.money += moneyCost;
        if (LanguageManager.Language == Language.EN)
        {
 text_result_level.text = $"Zombies killed: {levelStats.zombiesKilled}\nMoney: {levelStats.moneyCost}\nTime: {levelStats.timeLevelCompleting.ToLongTimeString()}";
        }

        else
        {
            text_result_level.text = $"Зомби убито: {levelStats.zombiesKilled}\nДенег заработано: {levelStats.moneyCost}\nВремя: {levelStats.timeLevelCompleting.ToLongTimeString()}";
        }
       
       
    }

    public void ExitToMenu()
    {
        SetTimeScale(1);
        Menu.OnPageLevels = true;
        Loading.OnLoad("Menu");
    }

   public void NextLevel ()
    {
        SetTimeScale(1);
        Loading.OnLoad(levelNextName);
    }

    


}
