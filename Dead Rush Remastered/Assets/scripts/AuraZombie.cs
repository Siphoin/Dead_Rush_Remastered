using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CircleCollider2D))]
public class AuraZombie : MonoBehaviour
{
    private List<ZombieBase> zombieList = new List<ZombieBase>();
    [SerializeField] private AuraZombieType typeAura;
    [SerializeField] private float speedAuraCall = 0.1f;

    private GameObject parent_go;
    // Use this for initialization
    void Start()
    {
        if (transform.parent == null)
        {
            throw new TransformParentNullExpection("parent aura null.");
        }
        if (speedAuraCall <= 0)
        {
            throw new System.ArgumentException("invalid speedAuraCall");
        }
        parent_go = transform.parent.gameObject;
        StartCoroutine(AuraCall());
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected IEnumerator AuraCall()
    {
        while (true)
        {
            yield return new WaitForSeconds(speedAuraCall);
            if (zombieList.Count > 0)
            {
                foreach (ZombieBase zombie in zombieList)
                {
switch (typeAura)
                {
                    case AuraZombieType.Speed:
                            zombie.Speed = (zombie.DefaultSpeed * 2);
                        break;
                    case AuraZombieType.Health:
                            if (zombie.Health < zombie.StartedHealth)
                            {
                                zombie.HillZombie(1);
                            }
                            break;
                }
                }
                
            }

        }

    }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            if (collision.gameObject != parent_go)
            {
            ZombieBase zombie = collision.GetComponent<ZombieBase>();
                if (typeAura == AuraZombieType.Speed)
                {
                    zombie.Speed = (zombie.DefaultSpeed * 2);
                }
            zombieList.Add(zombie);
            }

        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            if (collision.gameObject != parent_go)
            {
            ZombieBase zombie = collision.GetComponent<ZombieBase>();
            zombie.ReturnSpeedMovement();
            zombieList.Remove(zombie);
            }

        }


    }
    }