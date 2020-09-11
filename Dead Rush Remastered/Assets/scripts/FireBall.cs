using UnityEngine;

public class FireBall : Ball
{

    // Use this for initialization
    void Start()
    {
        FindBaricade();
        Vector3 pos_fire_baricade = transform.position;
        pos_fire_baricade.z = -2;
        transform.position = pos_fire_baricade;
    }

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
            baricade.Damage(damage);
            GameObject bullet = Instantiate(Resources.Load<GameObject>("Prefabs/fire_baricade"));
            bullet.transform.position = transform.position;
            Vector3 pos_fire_baricade = transform.position;
            pos_fire_baricade.x -= 1;
            pos_fire_baricade.z = -2;
            bullet.transform.position = pos_fire_baricade;
            Destroy(gameObject);


        }


        if (collision.tag == "Player")
        {
            LevelManager.manager.player.Damage(damage);
            LevelManager.manager.player.ShowAcidEffect();
            Destroy(gameObject);
        }

        if (collision.tag == "Partner")
        {
            Partner partner = collision.GetComponent<Partner>();
            partner.Damage(damage);
            partner.ShowAcidEffect();
            Destroy(gameObject);
        }
    }
}
