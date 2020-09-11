using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class ZombieBase : MonoBehaviour, IHPObject
{

    [Header("damage zombie")]
    [SerializeField] protected int damage;
    [Header("health zombie")]
    [SerializeField] private int health;
    [Header("armor zombie")]
    [SerializeField] private int armor;
    [Header("speed zombie")]
    [SerializeField] protected float speed;
    [Header("reward for murder")]
    [SerializeField] protected int rewardmurder;
    [Header("slider HP")]
    [SerializeField] protected Slider HP_slider;
    [Header("slider armor")]
    [SerializeField] protected Slider ARMOR_slider;
    const float CHECK_DISTANCE_DESTROY = 13f;

    protected Baricade baricade;

    protected bool move = true;

    public int Health { get => health; }
    public float Speed { get => speed; set => speed = value; }
    public bool Move { get => move; set => move = value; }
    public int RewardЬurder { get => rewardmurder; set => rewardmurder = value; }


    public event Action<int, bool, ZombieBase> deadEvent;


    protected void CheckParams()
    {
        HP_slider.value = health;
        if (ARMOR_slider != null)
        {
            ARMOR_slider.value = armor;
            if (armor <= 0)
            {
                Destroy(ARMOR_slider.gameObject);
            }
        }

        if (health <= 0)
        {
            GameObject blood = Instantiate(Resources.Load<GameObject>("Prefabs/blood"));
            blood.transform.position = transform.position;

            if (TryGetComponent(out BigZombie bigZombie))
            {
                blood.transform.localScale *= 2;
                bigZombie.Dispose();
            }
            deadEvent(rewardmurder, true, this);
            RewardDisplay rewardDisplay = Instantiate(Resources.Load<RewardDisplay>("Prefabs/reward_display"));
            rewardDisplay.OnDisplayReward(rewardmurder);
            rewardDisplay.transform.position = transform.position;
            Destroy(gameObject);
        }

        if (transform.position.x < Camera.main.transform.position.x)
        {

            if (Vector2.Distance(transform.position, Camera.main.transform.position) >= CHECK_DISTANCE_DESTROY)
            {
                deadEvent(rewardmurder, false, this);
                Destroy(gameObject);
            }
        }


    }

    public void Damage(int Damage)
    {
        if (armor > 0)
        {
            armor -= Damage;
        }

        else
        {
            health -= Damage;
        }

    }

    public void HillZombie(int value)
    {
        health += value;

    }

    protected void FindBaricade()
    {

        try
        {
            baricade = GameObject.FindGameObjectWithTag("Baricades").GetComponent<Baricade>();
        }

        catch
        {

        }

    }

    protected void IniStats()
    {
        LevelManager.manager.zombiieCount++;
        HP_slider.maxValue = health;

        if (armor > 0)
        {
            ARMOR_slider.maxValue = armor;
        }

        else
        {
            Destroy(ARMOR_slider.gameObject);
        }
    }

    public ZombieInfo GetInfo()
    {
        TypeAttackZombie typeAttackZombie = TypeAttackZombie.Near;
        if (damage == 0)
        {
            typeAttackZombie = TypeAttackZombie.Further;
        }

        return new ZombieInfo(armor, health, damage, typeAttackZombie, gameObject.name);
    }




}
