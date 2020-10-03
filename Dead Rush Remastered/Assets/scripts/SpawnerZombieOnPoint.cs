using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerZombieOnPoint : MonoBehaviour
{
    [SerializeField] private int max_count = 7;
    [SerializeField] private float time_spawn = 30f;
    [Header("point params")]
    [SerializeField] float x;
    [SerializeField] float y;

    private Color alpha_color { get; set; }

    private ZombieBase[] zombiesList;

    private SpriteRenderer mask;

    // Use this for initialization
    void Start()
    {
        var a_color = Color.white;
        a_color.a = 0;
        alpha_color = a_color;
        zombiesList = Resources.LoadAll<ZombieBase>("Prefabs/Zombies");
        mask = Resources.Load<SpriteRenderer>("Prefabs/zoimbie_mask");
        StartCoroutine(SpawnZombies());
    }


    private IEnumerator SpawnZombies()
    {
        while (true)
        {
            yield return new WaitForSeconds(time_spawn);
            int count = Random.Range(1, max_count + 1);
            while (count > 0)
            {
                yield return new WaitForSeconds(0.6f);
                float r_x = Random.Range(x * -1, x);
                float r_y = Random.Range(y * -1, y);
                float z = -2;
                Vector3 point = new Vector3(r_x, r_y, z);
                ZombieBase zombie = Instantiate(zombiesList[Random.Range(0, zombiesList.Length)]);
                zombie.transform.position = point;
                LevelManager.manager.OnRequstZombie(zombie);
                count--;
            }
        }
    }

}