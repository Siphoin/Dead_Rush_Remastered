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

    private DateTime timeEspliaded = new DateTime();

    private const float HEIGHT_SCREEN = 2.2f;




    private static LevelManager this_manager;

    private int moneyCost = 0;

    private int startHealthBaricade = 0;

    private Baricade baricade;

    [Header("new zombie info")]
    [SerializeField] private string name_zombie;

    private string PATH_INFO_ZOMBIES = "Assets/Resources/manifests/zombies_info.json";

    public static LevelManager manager { get => this_manager; }
    public Character player { get => Player; }
    public int level { get => currentLevel; }

    public int zombiieCount { get; set; } = 0;

    public event Action hordeEvent;
    public event Action<ZombieBase> bossEvent;

    private Character Player;

    private bool windowNewZombieExited = false;

    private bool levelIsFinished = false;

    // Use this for initialization

    void Start()
    {
        
        PATH_INFO_ZOMBIES = Resources.Load<TextAsset>("manifests/zombies_info").text;
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
yield return new WaitForSeconds(3);
            NewZombieWindow newZombieWindow = Instantiate(Resources.Load<NewZombieWindow>("Prefabs/NewZombieType"));
            ZombieInfoList infoList = JsonConvert.DeserializeObject<ZombieInfoList>(PATH_INFO_ZOMBIES);
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
        Player = Instantiate(Resources.Load<Character>("Prefabs/Characters/" + GameCache.player_cacheContainer.skin));
        baricade = Instantiate(Resources.Load<Baricade>("Prefabs/Baricades/" + GameCache.player_cacheContainer.baricades.name_prefab));
        Player.deadEvent += OnPlayerDead;
        Instantiate(Resources.Load<UIController>("Prefabs/UILevel"));
        if (GameCache.player_cacheContainer.partnerBuyed)
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
        }

    }

    // Update is called once per frame
    void Update()
    {

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
                    hordeEvent?.Invoke();
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
                    yield return new WaitForSeconds(UnityEngine.Random.Range(1, 3));
                }

                {

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

    private void NewZombie()
    {
        Vector3 position = new Vector3(15, UnityEngine.Random.Range(HEIGHT_SCREEN * -1, HEIGHT_SCREEN), -2);
        ZombieBase zombie = Instantiate(zombiesList[UnityEngine.Random.Range(0, variantsCountZombies)]);
        zombie.transform.position = position;
        currentZombiesSpawned++;
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
                }
            }

        }
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
}
