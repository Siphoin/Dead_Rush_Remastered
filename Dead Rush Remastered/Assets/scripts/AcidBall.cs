using UnityEngine;
using System.Collections;

public class AcidBall : Ball
{
    private bool boolean_anim = false;

    private float t_anim = 0;
    private Vector3 xScaleBall;
    private Vector3 startedScale;
    // Use this for initialization
    void Start()
    {
        startedScale = transform.localScale;
        xScaleBall = transform.localScale / 2;
        FindBaricade();
    }

    // Update is called once per frame
    void Update()
    {
        t_anim += 0.025f;
        if (boolean_anim)
        {
            transform.localScale = Vector3.Lerp(startedScale, xScaleBall, t_anim);
        }

        else
        {
            transform.localScale = Vector3.Lerp(xScaleBall, startedScale, t_anim);
        }

        if (t_anim >= 1)
        {
            boolean_anim = !boolean_anim;
            t_anim = 0;
        }
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

        if (collision.tag == "Partner")
        {
            Partner partner = collision.GetComponent<Partner>();
            partner.Damage(damage);
           partner.ShowAcidEffect();
            Destroy(gameObject);
        }
    }
}
