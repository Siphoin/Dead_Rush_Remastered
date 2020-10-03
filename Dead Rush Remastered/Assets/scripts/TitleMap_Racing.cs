using UnityEngine;

public class TitleMap_Racing : MonoBehaviour
{
    private CarCharacter car;

    private Zombie_Racing[] zombies;


    // Use this for initialization
    void Start()
    {
        zombies = Resources.LoadAll<Zombie_Racing>("Prefabs/Zombies/racing");
        car = GameObject.FindGameObjectWithTag("Player").GetComponent<CarCharacter>();
        if (Random.Range(0, 3) >= 1)
        {
            for (int i = 0; i < Random.Range(1, 7); i++)
            {
                float rad = 3f;
                Vector3 vecUnity = new Vector3(transform.position.x + rad * Mathf.Cos(Random.Range(-180, 180)), transform.position.y + rad * Mathf.Sin(Random.Range(-250, 250)), -2);
                Zombie_Racing zombie = Instantiate(zombies[Random.Range(0, zombies.Length)]);
                zombie.transform.position = vecUnity;
            }
            return;
        }

        if (Random.Range(0, 7) >= 2)
        {
            for (int i = 0; i < Random.Range(1, 7); i++)
            {
                float rad = 3f;
                Vector3 vecUnity = new Vector3(transform.position.x + rad * Mathf.Cos(Random.Range(-180, 180)), transform.position.y + rad * Mathf.Sin(Random.Range(-250, 250)), -2);
                Fire_Racing fire = Instantiate(Resources.Load<Fire_Racing>($"Prefabs/fire_racing"));
                fire.transform.position = vecUnity;
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(car.transform.position, transform.position) >= 20)
        {
            if (car.transform.position.x > transform.position.x)
            {
                Destroy(gameObject);
            }

        }
    }

}
