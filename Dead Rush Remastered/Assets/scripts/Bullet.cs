using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, Camera.main.transform.position) > 15)
        {
            Destroy(gameObject);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            collision.GetComponent<ZombieBase>().Damage(damage);
            Destroy(gameObject);
        }
    }
}
