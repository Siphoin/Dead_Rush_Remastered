using UnityEngine;
using System.Collections;

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
                Destroy(emotzu, 1.3f);
            }
        }

    }

    IEnumerator OnStand ()
    {
        yield return new WaitForSeconds(6);

        zombieBase.Move = false;
    }

}
