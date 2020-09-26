using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PartnerVisibleComponent : MonoBehaviour
{
    private List<ZombieBase> zombies = new List<ZombieBase>();

    public bool ZombiesEntered { get => zombies.Count > 0; }
    public ZombieBase End_zombie { get => zombies[0]; }

    // Use this for initialization
    void Start()
    {
       GameObject.FindGameObjectWithTag("Partner").GetComponent<Partner>().deadEvent += OnPartnerDead;
    }

    private void OnPartnerDead()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (zombies.Count > 1)
        {
            zombies.OrderBy(a => Vector2.Distance(a.transform.position, LevelManager.manager.player.transform.position)).First();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            
ZombieBase zombie = collision.GetComponent<ZombieBase>();
            if (!zombie.IsGhost)
            {
            zombie.deadEvent += OnDeadZombie;
            zombies.Add(zombie);
            }

            
                

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
            ZombieBase zombie = collision.GetComponent<ZombieBase>();
            if (zombies.Contains(zombie))
            {
            zombies.Remove(collision.GetComponent<ZombieBase>());
            }

        }
    }

}
