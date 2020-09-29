using Newtonsoft.Json;
using System;
using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int currentLevel = 1;
    [SerializeField] ZombieBase[] zombiesList;

    [SerializeField] int countZombiesInLevel = 1;
    [SerializeField] int bonus_count = 0;
    [SerializeField] ZombieBase boss;

    private int loopCountZombies = 1;

    private int currentZombiesSpawned = 0;

    private int variantsCountZombies = 0;

    private int zombieKilled = 0;

    private UIController UI;

    private DateTime timeEspliaded = new DateTime();

    private const float HEIGHT_SCREEN = 3f;




    private static LevelManager this_manager;

    private int moneyCost = 0;

    private int startHealthBaricade = 0;

    private Baricade baricade;

    [Header("new zombie info")]
    [SerializeField] private string name_zombie;

    [Header("dialog scene name")]


 [SerializeField]   private string dialogNameScene = null;

    private string JSON_INFO_ZOMBIES;

    public static LevelManager manager { get => this_manager; }
    public Character player { get => Player; }
    public int level { get => currentLevel; }

    public int zombiieCount { get; set; } = 0;

    public static int MAX_LEVEL_GAME => mAX_LEVEL_GAME;

    public event Action HordeEvent;
    public event Action<ZombieBase> bossEvent;

    private Character Player;

    private bool windowNewZombieExited = false;

    private bool levelIsFinished = false;

    private const int mAX_LEVEL_GAME = 50;

    // Use this for initialization

    void Start()
    {
        
        if (currentLevel <= 0)
        {
            throw new UnityException("current this Level <= 0!");
        }

        if (countZombiesInLevel <= 0)
        {
            throw new UnityException("current count zombies in level <= 0!");
        }

        if (bonus_count < 0)
        {
            throw new UnityException("bonus_count < 0!");
        }
        if (zombiesList.Length == 0)
        {
            zombiesList = Resources.LoadAll<ZombieBase>("Prefabs/Zombies");
        }

        if (zombiesList.Length == currentLevel)
        {
            variantsCountZombies = currentLevel;
        }

        else
        {
            variantsCountZombies = zombiesList.Length;
        }
        try
        {
            startHealthBaricade = baricade.health;
        }

        catch
        {

        }
        if (boss != null)
        {
            countZombiesInLevel++;
        }
        BlackTransitionCaller.Create();
        StartCoroutine(StartLevel());
        Instantiate(Resources.Load<MusicPlayer>("music_prefabs/norm_level_music"));
    }

    IEnumerator StartLevel()
    {
        if (!string.IsNullOrEmpty(name_zombie))
        {
            if (!GameCache.ContainsZombieInBook(name_zombie))
            {
                JSON_INFO_ZOMBIES = Resources.Load<TextAsset>("manifests/zombies_info").text;
                yield return new WaitForSeconds(3);
            NewZombieWindow newZombieWindow = Instantiate(Resources.Load<NewZombieWindow>("Prefabs/NewZombieType"));
            ZombieInfoList infoList = JsonConvert.DeserializeObject<ZombieInfoList>(JSON_INFO_ZOMBIES);
            newZombieWindow.SetInfo(infoList.zombieList[name_zombie]);
            newZombieWindow.exitEvent += OnWindowNewZombieExit;
                infoList.Dispose();
            while (windowNewZombieExited == false)
            {
                yield return new WaitForSeconds(1 / 60);
            }
            }
            

        }
        StartCoroutine(SpawnZombie());
        StartCoroutine(BonusCountZombies());
        StartCoroutine(TimeTickLevel());
    }

    private void OnWindowNewZombieExit()
    {
        windowNewZombieExited = true;
    }

    private void Awake()
    {
        this_manager = this;
        if (currentLevel <= 0)
        {
            throw new UnityException("current this Level <= 0!");
        }

        if (countZombiesInLevel <= 0)
        {
            throw new UnityException("current count zombies in level <= 0!");
        }

        if (bonus_count < 0)
        {
            throw new UnityException("bonus_count < 0!");
        }
        Player = Instantiate(Resources.Load<Character>("Prefabs/Characters/" + GameCache.Player_cacheContainer.skin));
        baricade = Instantiate(Resources.Load<Baricade>("Prefabs/Baricades/" + GameCache.Player_cacheContainer.baricades.name_prefab));
        Player.deadEvent += OnPlayerDead;
    UI =    Instantiate(Resources.Load<UIController>("Prefabs/UILevel"));
        if (GameCache.Player_cacheContainer.partnerBuyed)
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/PartnerVisibleComponent"));
            Instantiate(Resources.Load<Partner>("Prefabs/partner"));
        }

    }

    private void OnPlayerDead()
    {
        if (!levelIsFinished)
        {
            ResultLevelWindow result = Instantiate(Resources.Load<ResultLevelWindow>("Prefabs/ResultWindowLevel"));
            LevelIProgressData data = new LevelIProgressData(currentLevel, 0);
            LevelStats levelStats = new LevelStats(zombieKilled, timeEspliaded, moneyCost);
            result.SetState(ResultLevel.Defeat, 0, moneyCost, data, levelStats);
            levelIsFinished = true;
            FrezzeMechanimsLevel();
            WriteOnStatictics(ResultLevel.Defeat);
        }

    }


    IEnumerator SpawnZombie()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(2, 7));
            int loop_count_zombies = UnityEngine.Random.Range(1, loopCountZombies + 1);
            for (int i = 0; i < loop_count_zombies; i++)
            {
                NewZombie();
                if (currentZombiesSpawned == countZombiesInLevel - (countZombiesInLevel / 2))
                {
                    while (zombiieCount > 0)
                    {
                        yield return new WaitForSeconds(1 / 60);
                    }
                    yield return new WaitForSeconds(2);
                    HordeEvent?.Invoke();
                    yield return new WaitForSeconds(3);
                    for (int j = 0; j < countZombiesInLevel / 2; j++)
                    {
                        yield return new WaitForSeconds(0.5f);
                        NewZombie();
                    }
                    if (boss != null)
                    {
                        yield return new WaitForSeconds(2);
                        SpawnBoss();
                    }
                    yield break;
                }
                if (loop_count_zombies > 1)
                {
                    yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 3));
                }


            }





        }
    }

    IEnumerator TimeTickLevel()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (zombiieCount == 0)
            {
                if (currentZombiesSpawned == countZombiesInLevel)
                {
                    yield break;

                }
            }

            timeEspliaded = timeEspliaded.AddSeconds(1);

        }
    }

    private void FrezzeMechanimsLevel ()
    {
        Destroy(UI.gameObject);
        Player.enabled = false;
        StopAllCoroutines();
    }

    private void NewZombie(bool accounting = true, ZombieBase custom_prefab = null)
    {
        Vector3 position = new Vector3(15, UnityEngine.Random.Range(HEIGHT_SCREEN * -1, HEIGHT_SCREEN), -2);
        ZombieBase zombie = null;
        if (custom_prefab == null)
        {
         zombie = Instantiate(zombiesList[UnityEngine.Random.Range(0, variantsCountZombies)]);
        }

        else {
            zombie = Instantiate(custom_prefab);

        }

        zombie.transform.position = position;
        if (countZombiesInLevel != int.MaxValue)
        {
            if (accounting)
            {
currentZombiesSpawned++;
            }
        
        }

        zombie.deadEvent += OnDeadZombie;
    }

    private void SpawnBoss()
    {
        Vector3 position = new Vector3(15, UnityEngine.Random.Range(HEIGHT_SCREEN * -1, HEIGHT_SCREEN), -2);
        ZombieBase zombie = Instantiate(boss);
        zombie.transform.position = position;
        currentZombiesSpawned++;
        zombie.deadEvent += OnDeadZombie;
        bossEvent?.Invoke(zombie);
    }

    private void OnDeadZombie(int obj, bool obj2, ZombieBase obj3)
    {
        zombiieCount--;
        if (obj2)
        {
            zombieKilled++;
            moneyCost += obj;
        }


        if (zombiieCount == 0)
        {
            if (currentZombiesSpawned >= countZombiesInLevel)
            {
                int stars = 3;
                if (baricade != null)
                {
                    if (baricade.health < (startHealthBaricade / 2))
                    {
                        stars--;
                    }
                }

                else
                {
                    stars--;
                }


                if (Player.Health < 100)
                {
                    stars--;
                }
                if (zombieKilled < (currentZombiesSpawned / 2))
                {
                    stars--;
                }
                if (zombieKilled == 0)
                {
                    stars = 0;
                }

                if (!levelIsFinished)
                {
                    ResultLevelWindow result = Instantiate(Resources.Load<ResultLevelWindow>("Prefabs/ResultWindowLevel"));
                    LevelIProgressData data = new LevelIProgressData(currentLevel, stars);
                    LevelStats levelStats = new LevelStats(zombieKilled, timeEspliaded, moneyCost);
                    result.SetState(ResultLevel.Victory, stars, moneyCost, data, levelStats);
                    levelIsFinished = true;
                    FrezzeMechanimsLevel();
                    if (!string.IsNullOrEmpty(dialogNameScene))
                    {
                        result.SetSceneDialogName(dialogNameScene);
                    }

                    WriteOnStatictics(ResultLevel.Victory);
                    StatisticsCache.SaveData();
                }
            }

        }
    }

    private void WriteOnStatictics(ResultLevel result)
    {
        switch (result)
        {
            case ResultLevel.Victory:
                StatisticsCache.Statistics.victory_score++;
                break;
            case ResultLevel.Defeat:
                StatisticsCache.Statistics.defeats_score++;
                break;
        }

        StatisticsCache.Statistics.zombie_kills_score += zombieKilled;
        StatisticsCache.Statistics.money_earned += moneyCost;
        StatisticsCache.SaveData();
    }

    IEnumerator BonusCountZombies()
    {
        while (true)
        {
            yield return new WaitForSeconds(18);
            if (currentZombiesSpawned < countZombiesInLevel)
            {
                if (bonus_count > 1)
                {
                    loopCountZombies *= bonus_count;
                }

            }


        }


    }

    public void OnRequstZombie(ZombieBase target)
    {
        target.deadEvent += OnDeadZombie;
    }

    public void SetCustomParams (int count_zombies)
    {
        countZombiesInLevel = count_zombies;
    }

    public void CallCustomHorde (int countZombies, ZombieBase prefab_zombie = null)
    {
        StartCoroutine(CallCustomHorde_IEnumerator(countZombies, prefab_zombie));
    }

    private IEnumerator CallCustomHorde_IEnumerator (int c, ZombieBase zombie)
    {
        
        while (c > 0)
        {
            float r = UnityEngine.Random.Range(0.5f, 2);
            yield return new WaitForSeconds(r);
            NewZombie(false, zombie);
            c--;
        }
    }
}
