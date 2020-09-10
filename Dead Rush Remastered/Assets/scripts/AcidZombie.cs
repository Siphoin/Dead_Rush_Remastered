using System.Collections;
using UnityEngine;

public class AcidZombie : ZombieBase
{
    [SerializeField] protected float ATTACK_RANGE = 5;
    // Use this for initialization
    void Start()
    {
        IniStats();
        FindBaricade();
        StartCoroutine(SplitAcidTick());
    }

    // Update is called once per frame
    void Update()
    {
        if (baricade != null)
        {
            float dist = Vector2.Distance(transform.position, baricade.transform.position);
            move = dist > ATTACK_RANGE;
        }



        if (move)
        {
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);
        }

        CheckParams();

    }

    IEnumerator SplitAcidTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 4));
            if (baricade != null || LevelManager.manager.player != null)
            {
                if (!move)
                {
                    CreateBall();
                }

            }

            if (baricade == null && LevelManager.manager.player == null)
            {
                move = true;
                yield break;
            }


        }
    }


    private void CreateBall()
    {
        GameObject bullet = Instantiate(Resources.Load<GameObject>("Prefabs/acid_effect"));
        bullet.transform.position = transform.position;
    }
}
