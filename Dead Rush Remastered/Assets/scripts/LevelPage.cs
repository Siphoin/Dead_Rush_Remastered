using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelPage : MonoBehaviour
{
    [SerializeField] int countLevels;
    LevelButton example_prefab_levelButton;
    List<LevelButton> buttons = new List<LevelButton>();
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

        for (int i = 0; i < GameCache.cacheContainer.levelCompleted; i++)
        {
            if (GameCache.cacheContainer.levelsData.ContainsKey(i + 1)) { 
            buttons[i].SetDataLevel(GameCache.cacheContainer.levelsData[i + 1]);
            }

            
        }
        if (GameCache.cacheContainer.levelCompleted > 0)
        {
        int levelOpened = GameCache.cacheContainer.levelCompleted;
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
        }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
