using System.Collections;
using UnityEngine;

public class RegenerationZombie : MonoBehaviour
{
    private ZombieBase zombieBase;
    private int startedHealth;
    const int REGENERATION_TIME = 2;
    // Use this for initialization
    void Start()
    {
        zombieBase = GetComponent<ZombieBase>();
        startedHealth = zombieBase.Health;
        StartCoroutine(Hill());
    }

    private IEnumerator Hill()
    {
        while (true)
        {
            yield return new WaitForSeconds(REGENERATION_TIME);
            if (zombieBase.Health < startedHealth)
            {
                zombieBase.HillZombie(1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
