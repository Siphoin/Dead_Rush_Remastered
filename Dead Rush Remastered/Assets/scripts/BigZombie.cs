using UnityEngine;
using System.Collections;
using System;
using Random = UnityEngine.Random;

public class BigZombie : MonoBehaviour, IDisposable
{
    const int COUNT_ZOMBIES = 5;
    const string NAME_PREFAB_ZOMBIE = "Prefabs/Zombies/zombie1";
    private ZombieBase zombieBase;
 private   Vector3 pos_zombie;
    // Use this for initialization
    void Start()
    {
        
        zombieBase = GetComponent<ZombieBase>();
    }

    // Update is called once per frame
    void Update()
    {
        pos_zombie = transform.position;
        if (zombieBase.Health <= 1)
        {
            for (int i = 0; i < COUNT_ZOMBIES; i++)
            {
                float rad = 1f;
                Vector3 vecUnity = new Vector3(pos_zombie.x + rad * Mathf.Cos(Random.Range(-180, 180)), pos_zombie.y + rad * Mathf.Sin(Random.Range(-250, 250)), -2);
                GameObject zombie = Instantiate(Resources.Load<GameObject>("Prefabs/Zombies/zoimbie1"));
                zombie.transform.position = vecUnity;
            }

            zombieBase.DamageZombie(int.MaxValue);
        }
        
    }

    private void OnDestroy()
    {
    
    }

    public void Dispose()
    {
        pos_zombie = Vector3.zero;
        zombieBase = null;
    }
}
