using UnityEngine;
using System.Collections;

public class Fire_Racing : MonoBehaviour
{

    [SerializeField] private int damage;
    private CarCharacter car;

    // Use this for initialization
    void Start()
    {
        car = GameObject.FindGameObjectWithTag("Player").GetComponent<CarCharacter>();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.tag == "Player")
        {
            CarCharacter car = collision.GetComponent<CarCharacter>();
            car.Health -= damage;
            car.ShowAcidEffect();
            Destroy(gameObject);

        }
    }
}
