using UnityEngine;
using System.Collections;
using System;

public class LevelManager_Racing : MonoBehaviour
{
 private   CarCharacter car;

    private UIController_Racing uIController;



    const int SHAGE_X_NEW_TITLEMAP = 21;
    const int SHAGE_Y_NEW_TITLEMAP = 10;
    const int SHAGE_X_CAR = 9;
    private int startedXTitleMap = 0;
    private int startedYTitleMap = 0;

  static  LevelManager_Racing Manager;

    private float startedXCar;


    private int money = 0;

    private int zombie_count_kill = 0;

    private bool levelIsFinished = false;


    private GameObject[] titleMaps;

    public event Action zombieKillEvent;
    
    public CarCharacter Car { get => car; }
    public static LevelManager_Racing manager { get => Manager; }
    public int ZombieKillCount { get => zombie_count_kill; }
    public int ZombiesMax { get => zombiesMax; }

    [SerializeField] private int zombiesMax = 100;
    [SerializeField] private int currentLevel = 11;
    [SerializeField] private int minuteTime = 5;
    private DateTime timeEspliaded;
    private DateTime timer;

    public event Action OnEndLevel;

    public string timerString
    {
        get
        {
            string min = string.Format("{00:00}", timer.Minute);
            string sec = string.Format("{00:00}", timer.Second);
            return $"{min}:{sec}";
        }
    }
    // Use this for initialization
    void Start()
    {
        titleMaps = Resources.LoadAll<GameObject>("Prefabs/racing_titlemaps");
        car = Instantiate(Resources.Load<CarCharacter>("Prefabs/car_player"));
        startedXCar = car.transform.position.x;
        car.deadEvent += OnPlayerDead;
        car.killZombie += CarKillZombie;
        timer = timer.AddMinutes(minuteTime);

        for (int i = 0; i < 4; i++)
        {
            NextGenerate();

        }

        StartCoroutine(TimeTickLevel());
        BlackTransitionCaller.Create();
        Instantiate(Resources.Load<MusicPlayer>("music_prefabs/level_music_racing"));
    }

    private void CarKillZombie(int obj)
    {
        money += obj;
        zombie_count_kill++;
        zombieKillEvent?.Invoke();

        if (zombiesMax <= zombie_count_kill)
        {
            int stars = 3;
            if (car.Health <= 99)
            {
                stars--;
            }
            if (car.Health <= 50)
            {
                stars--;
            }

            if (car.Health <= 25)
            {
                stars--;
            }
            if (!levelIsFinished)
            {
car.enabled = false;
            ResultLevelWindow result = Instantiate(Resources.Load<ResultLevelWindow>("Prefabs/ResultWindowLevel"));
            LevelIProgressData data = new LevelIProgressData(currentLevel, stars);
            LevelStats levelStats = new LevelStats(zombie_count_kill, timeEspliaded, money);
            result.SetState(ResultLevel.Victory, stars, money, data, levelStats);
            StopAllCoroutines();
                levelIsFinished = true;
                OnEndLevel?.Invoke();
                WriteOnStatictics(ResultLevel.Victory);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
       if (car.transform.position.x >= startedXCar)
        {
            NextGenerate();
        }
    }
    private void NextGenerate ()
    {
        Vector3[] vectors = new Vector3[]
        {
             new Vector3(startedXTitleMap, 0),
             new Vector3(startedXTitleMap, SHAGE_Y_NEW_TITLEMAP),
             new Vector3(startedXTitleMap, SHAGE_Y_NEW_TITLEMAP * -1),
    };

        for (int i = 0; i < vectors.Length; i++)
        {
            GameObject titleMap = Instantiate(titleMaps[UnityEngine.Random.Range(0, titleMaps.Length)]);
            titleMap.transform.position = vectors[i];
        }
       

        startedXTitleMap += SHAGE_X_NEW_TITLEMAP;
        startedYTitleMap += SHAGE_Y_NEW_TITLEMAP;
        startedXCar += SHAGE_X_CAR;          
        }

    private void Awake()
    {
        Manager = this;
        uIController = Instantiate(Resources.Load<UIController_Racing>("Prefabs/UILevel_Racing"));
    }

    private void OnPlayerDead()
    {
        if (!levelIsFinished)
        {
            ResultLevelWindow result = Instantiate(Resources.Load<ResultLevelWindow>("Prefabs/ResultWindowLevel"));
            LevelIProgressData data = new LevelIProgressData(currentLevel, 0);
            LevelStats levelStats = new LevelStats(zombie_count_kill, timeEspliaded, money);
            result.SetState(ResultLevel.Defeat, 0, money, data, levelStats);
            levelIsFinished = true;
            OnEndLevel?.Invoke();
            WriteOnStatictics(ResultLevel.Defeat);
        }

        }

    IEnumerator TimeTickLevel()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (car.Health <= 0)
            {
                yield break;
            }

            if (timer.Minute == 0)
            {
                if (timer.Second == 0)
                {
                    yield break;
                }

            }

                    timeEspliaded = timeEspliaded.AddSeconds(1);
            timer = timer.AddSeconds(-1);

            if (timer.Minute == 0)
            {
                if(timer.Second == 0)
                {
                   if (!levelIsFinished)
                    {
                    car.enabled = false;
                    ResultLevelWindow result = Instantiate(Resources.Load<ResultLevelWindow>("Prefabs/ResultWindowLevel"));
                    LevelIProgressData data = new LevelIProgressData(currentLevel, 0);
                    LevelStats levelStats = new LevelStats(zombie_count_kill, timeEspliaded, money);
                    result.SetState(ResultLevel.Defeat, 0, money, data, levelStats);
                        levelIsFinished = true;
                        WriteOnStatictics(ResultLevel.Defeat);
                    }

                    
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

        StatisticsCache.Statistics.zombie_kills_score += zombie_count_kill;
        StatisticsCache.Statistics.money_earned += money;
        StatisticsCache.SaveData();
    }

}

