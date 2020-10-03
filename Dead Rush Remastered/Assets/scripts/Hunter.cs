using System.Collections;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    private ZombieBase zombieBase;

    Vector3 target_point;

    private const float SPEED_JUMP = 0.01f / 60f;

    private Transform target;

    // Use this for initialization
    void Start()
    {
        int random_target = Random.Range(0, 2);

        if (random_target == 0)
        {
            if (LevelManager.manager.player != null)
            {
                FindPlayer();
            }

            else
            {
                FindPartner();
            }
        }

        else
        {
            FindPartner();

            if (target == null)
            {
                FindPlayer();
            }
        }


        zombieBase = GetComponent<ZombieBase>();
        if (target != null)
        {
            StartCoroutine(Teleport());
            StartCoroutine(OnStand());
        }

    }

    private void FindPlayer()
    {
        try
        {
            target = LevelManager.manager.player.transform;
        }

        catch
        {
            target = null;
        }

    }

    private void FindPartner()
    {
        try
        {
            target = GameObject.FindGameObjectWithTag("Partner").transform;
        }

        catch
        {
            target = null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            target_point = new Vector3(target.position.x, target.position.y, transform.position.z);
        }

    }


    private IEnumerator Teleport()
    {
        yield return new WaitForSeconds(14.5f);
        float t_j = 0;
        zombieBase.Speed = 0;
        while (t_j < 1)
        {
            yield return new WaitForSeconds(SPEED_JUMP);
            t_j += SPEED_JUMP;
            transform.position = Vector3.Lerp(transform.position, target_point, t_j);

            if (t_j >= 1)
            {
                zombieBase.ReturnSpeedMovement();
                yield break;
            }
        }
    }

    IEnumerator OnStand()
    {
        yield return new WaitForSeconds(10);

        zombieBase.Move = false;
    }
}
