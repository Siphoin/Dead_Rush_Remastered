using System.Collections;
using UnityEngine;

public class FireZombie : AcidZombie
{

    void Start()
    {
        IniStats();
        FindBaricade();
        StartCoroutine(SplitFireTick());
    }

    // Update is called once per frame
    void Update()
    {
        CheckParams();
        
        if (baricade != null)
        {
            float dist = Vector2.Distance(transform.position, baricade.transform.position);
            move = dist > ATTACK_RANGE;


        }

        else
        {
            if (LevelManager.manager.player != null)
            {
                float dist = Vector2.Distance(transform.position, LevelManager.manager.player.transform.position);
                move = dist > ATTACK_RANGE;
            }

            else
            {
                move = true;
            }
        }


    }

    IEnumerator SplitFireTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(5.3F);
            if (baricade != null || LevelManager.manager.player != null)
            {
                if (!move)
                {
                    CreateBalls();
                }

            }

            if (baricade == null && LevelManager.manager.player == null)
            {
                move = true;
                yield break;
            }
        }
    }

    private void CreateBalls()
    {
        Vector3[] angles = new Vector3[]
                        {
                    Vector3.zero,
                    new Vector3(0, 0, -38 + Random.Range(0f, 3f)),
                    new Vector3(0, 0, 31 + Random.Range(0f, 3f))
                        };
        for (int i = 0; i < angles.Length; i++)
        {
            GameObject bullet = Instantiate(Resources.Load<GameObject>("Prefabs/fireball_effect"));
            bullet.transform.position = transform.position;
            bullet.transform.eulerAngles = angles[i];
        }
    }
}
