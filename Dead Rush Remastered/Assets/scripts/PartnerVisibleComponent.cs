using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class PartnerVisibleComponent : MonoBehaviour
{
    private List<ZombieBase> zombies = new List<ZombieBase>();

    public bool ZombiesEntered { get => zombies.Count > 0; }
    public ZombieBase End_zombie { get => zombies[0]; }

    // Use this for initialization
    void Start()
    {
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (ZombiesEntered && LevelManager.manager.player != null)
        {
            zombies.OrderBy(a => Vector2.Distance(a.transform.position, LevelManager.manager.player.transform.position)).First();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            ZombieBase zombie = collision.GetComponent<ZombieBase>();
            zombie.deadEvent += OnDeadZombie;
            zombies.Add(zombie);
           
        }
    }

    private void OnDeadZombie(int arg1, bool arg2, ZombieBase arg3)
    {
        zombies.Remove(arg3);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            zombies.Remove(collision.GetComponent<ZombieBase>());
        }
    }

    }
