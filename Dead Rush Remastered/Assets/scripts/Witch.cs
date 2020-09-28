﻿using System.Collections;
using UnityEngine;

public class Witch : MonoBehaviour
{
    float startedSpeed;
    int startedHealth;
    bool extraMoved = false;
    private ZombieBase zombieBase;
    [SerializeField] GameObject emotzu;
    // Use this for initialization
    void Start()
    {
        emotzu.SetActive(false);
        zombieBase = GetComponent<ZombieBase>();
        startedSpeed = zombieBase.Speed;
        startedHealth = zombieBase.Health;
        StartCoroutine(OnStand());

    }

    // Update is called once per frame
    void Update()
    {
        if (!extraMoved)
        {
            if (zombieBase.Health != startedHealth)
            {
                zombieBase.Move = true;
                extraMoved = true;
                StopAllCoroutines();
                zombieBase.Speed = startedSpeed * 5;
                emotzu.SetActive(true);
                Instantiate(Resources.Load<AudioSource>("fx_prefabs/witch_damage_audio"));
                Destroy(emotzu, 1.3f);
            }

        }

    }

    IEnumerator OnStand()
    {
        yield return new WaitForSeconds(6);
        zombieBase.Move = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            if (!extraMoved)
            {
                zombieBase.Move = false;
            }
        }

        if (collision.tag == "Baricades")
        {
                extraMoved = true;
            
        }


    }


}
