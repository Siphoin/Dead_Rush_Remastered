using UnityEngine;
using System.Collections;

public class AcidBall : Ball
{



    // Use this for initialization
    void Start()
    {
        FindBaricade();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * -1 * speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, Camera.main.transform.position) > 15)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Baricades")
        {
            baricade.DamageBaricade(damage);
            Destroy(gameObject);
        }

        if (collision.tag == "Player")
        {
            LevelManager.manager.player.Damage(damage);
            LevelManager.manager.player.ShowAcidEffect();
            Destroy(gameObject);
        }
    }
}
