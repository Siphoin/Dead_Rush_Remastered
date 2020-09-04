using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{
    [Header("hp bar")]
    [SerializeField] Slider HPBar;
    [Header("hp baricades")]
    [SerializeField] Slider HPBaricades;
    private Baricade baricade;
    [Header("level Text")]
    [SerializeField] TransliteText levelNumText;
    // Use this for initialization
    void Start()
    {
        LevelManager.manager.hordeEvent += HordeOn;
        baricade = GameObject.FindGameObjectWithTag("Baricades").GetComponent<Baricade>();
        HPBaricades.maxValue = baricade.health;
        string str_translite_num_level = $" {LevelManager.manager.level}";
        levelNumText.SetupText(str_translite_num_level, str_translite_num_level);

        
    }

    private void HordeOn()
    {
          Instantiate(Resources.Load<GameObject>("Prefabs/Text_wave"), transform);
    }

    // Update is called once per frame
    void Update()
    {
      if (baricade != null)
        {
            HPBaricades.value = baricade.health;
        }

        if (LevelManager.manager.player != null)
        {
            HPBar.value = LevelManager.manager.player.Health;
        }
    }
}
