using UnityEngine;
using System.Collections;

public class Hunter : MonoBehaviour
{
    private ZombieBase zombieBase;

    // Use this for initialization
    void Start()
    {
        zombieBase = GetComponent<ZombieBase>();
        StartCoroutine(Teleport());
        StartCoroutine(OnStand());
    }

    // Update is called once per frame
    void Update()
    {

    }


 private   IEnumerator Teleport ()
    {
        yield return new WaitForSeconds(12.5f);
        Vector3 vecPlayer = LevelManager.manager.player.transform.position;
        transform.position = new Vector3(vecPlayer.x, vecPlayer.y, transform.position.z);
    }

    IEnumerator OnStand()
    {
        yield return new WaitForSeconds(10);

        zombieBase.Move = false;
    }
}
