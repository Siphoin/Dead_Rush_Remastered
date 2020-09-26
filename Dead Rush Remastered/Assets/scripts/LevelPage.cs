using System.Collections.Generic;
using UnityEngine;

public class LevelPage : MonoBehaviour
{
    [SerializeField] int countLevels;
    LevelButton example_prefab_levelButton;
    List<LevelButton> buttons = new List<LevelButton>();
    [SerializeField] string[] dialogsScenesNames;
    // Use this for initialization
    void Start()
    {
        example_prefab_levelButton = Resources.Load<LevelButton>("Prefabs/level_button");
        for (int i = 1; i < countLevels + 1; i++)
        {
            LevelButton button = Instantiate(example_prefab_levelButton, transform);
            button.levelIndex = i;
            buttons.Add(button);
        }

        for (int i = 0; i < GameCache.Player_cacheContainer.levelCompleted; i++)
        {
            if (GameCache.Player_cacheContainer.levelsData.ContainsKey(i + 1))
            {
                buttons[i].SetDataLevel(GameCache.Player_cacheContainer.levelsData[i + 1]);
            }


        }
        if (GameCache.Player_cacheContainer.levelCompleted > 0)
        {
            int levelOpened = GameCache.Player_cacheContainer.levelCompleted;
            int indexBossLevel = 10;
            for (int i = 1; i < buttons.Count; i++)
            {

                if (levelOpened < buttons[i].levelIndex)
                {
                    buttons[i].BlockButton();
                }


                else
                {
                    if (i + 1 == indexBossLevel)
                    {
                        indexBossLevel += 10;
                        buttons[i].SetBossLevel();
                    }
                }

                if (!string.IsNullOrEmpty(dialogsScenesNames[i]))
                {
                    buttons[i].SetSceneDialog(dialogsScenesNames[i]);
                }
            }
        }

    }
}
