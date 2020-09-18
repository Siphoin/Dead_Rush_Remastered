using UnityEngine;
using System.Collections;
using System;

public class Zombie_Racing : MonoBehaviour, IDieAudio
{
   [SerializeField] private float speed;
 [SerializeField]   private int rewardmurder;
    private CarCharacter car;

    public int Rewardmurder { get => rewardmurder; }

    // Use this for initialization
    void Start()
    {
        car = GameObject.FindGameObjectWithTag("Player").GetComponent<CarCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * -1 * speed * Time.deltaTime);

        if (Vector2.Distance(car.transform.position, transform.position) >= 20)
        {
            if (car.transform.position.x > transform.position.x)
            {
            Destroy(gameObject);
            }

        }
    }


    public void OnDead()
    {
        GameObject blood = Instantiate(Resources.Load<GameObject>("Prefabs/blood"));
        blood.transform.position = transform.position;
        RewardDisplay rewardDisplay = Instantiate(Resources.Load<RewardDisplay>("Prefabs/reward_display"));
        rewardDisplay.OnDisplayReward(rewardmurder);
        rewardDisplay.transform.position = transform.position;
        PlayAudioDie();
        Destroy(gameObject);
    }

    public void PlayAudioDie()
    {
        Instantiate(Resources.Load<AudioSource>("fx_prefabs/zombie_die_audio"));
    }
}
