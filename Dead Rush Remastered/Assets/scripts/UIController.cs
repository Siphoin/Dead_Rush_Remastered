using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("hp bar")]
    [SerializeField] Slider HPBar;
    [Header("hp baricades")]
    [SerializeField] Slider HPBaricades;
    [Header("boss hp")]
    [SerializeField] Slider HPBoss;
    private Baricade baricade;
    private ZombieBase boss_target;
    [Header("level Text")]
    [SerializeField] TransliteText levelNumText;
    // Use this for initialization
    void Start()
    {
        LevelManager.manager.HordeEvent += HordeOn;
        LevelManager.manager.bossEvent += BossOn;
        baricade = GameObject.FindGameObjectWithTag("Baricades").GetComponent<Baricade>();
        HPBaricades.maxValue = baricade.health;
        string str_translite_num_level = $" {LevelManager.manager.level}";
        levelNumText.SetupText(str_translite_num_level, str_translite_num_level);

        if (LevelManager.manager.level > LevelManager.MAX_LEVEL_GAME)
        {
            switch (LanguageManager.Language)
            {
                case Language.EN:
                    str_translite_num_level = "SURVIVAL";
                    break;
                case Language.RU:
                    str_translite_num_level = "ВЫЖИВАНИЕ";
                    break;
            }
            levelNumText.text = str_translite_num_level;
        }

        
            HPBoss.gameObject.SetActive(false);


        }

        private void BossOn(ZombieBase obj)
    {
        boss_target = obj;
        HPBoss.maxValue = boss_target.Health;
        HPBoss.value = boss_target.Health;
        HPBoss.gameObject.SetActive(true);
        obj.deadEvent += OnBossDead;
    }

    private void OnBossDead(int arg1, bool arg2, ZombieBase arg3)
    {
        HPBoss.gameObject.SetActive(false);
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

        if (boss_target != null)
        {
            HPBoss.value = boss_target.Health;
        }

    }
    }