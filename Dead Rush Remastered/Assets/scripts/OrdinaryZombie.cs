using UnityEngine;
using System.Collections;

public class OrdinaryZombie : ZombieBase
{
    bool enteredBaricades;
    bool onDamagePlayer;
    [Header("damage cooldown")]
    [SerializeField] float cooldownDamage;

    public bool EnteredBaricades { get => enteredBaricades; }

    // Use this for initialization
    void Start()
    {
        IniStats();
        FindBaricade();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownDamage <= 0)
        {
            throw new UnityException("Cooldown damage <= 0 Object: " + gameObject.name);
        }
        CheckParams();
        if (move)
        {
        transform.Translate(transform.right * -1 * speed * Time.deltaTime);
        }

        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Baricades":
                StartCoroutine(DamageTick());
                break;

            case "Zombie":
                if (collision.TryGetComponent(out OrdinaryZombie zombie))
                {
                    if (zombie.enteredBaricades)
                    {
                        move = false;
                    }

                }


                break;


            case "Player":
                onDamagePlayer = true;
                StartCoroutine(DamageTickPlayer());
                break;
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Baricades":
                move = false;
                enteredBaricades = true;
                break;

            case "Player":
                move = false;
                break;

        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
switch (collision.tag)
        {
            case "Baricades":
                StopCoroutine(DamageTick());
                move = true;
                enteredBaricades = false;
                break;

            default:
                if (collision.tag != "Bullet")
                move = true;

                if (collision.tag == "Player")
                {
                    onDamagePlayer = false;
                    StopCoroutine(DamageTickPlayer());
                }
                break;
        }
    }

   IEnumerator DamageTick ()
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldownDamage);
            baricade.DamageBaricade(damage);
        //    Debug.Log(baricade.health);
        }
    }

    IEnumerator DamageTickPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.5f);
            if (!onDamagePlayer)
            {
                yield break;
            }
            LevelManager.manager.player.Damage(damage);
        }
    }
}
