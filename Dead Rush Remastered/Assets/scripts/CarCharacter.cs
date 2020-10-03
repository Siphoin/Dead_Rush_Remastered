using UnityEngine;
using System.Collections;
using System;

public class CarCharacter : Character, ICharacter
{
    new float HEIGHT_SCREEN = 9.75f;

    private int statusBlood = 0;

 [SerializeField]   private Sprite[] statusCarSprites;

    private int MAX_STATUS_CAR;

    private SpriteRenderer spriteRenderer;

    public event Action<int> killZombie;

    [SerializeField] GameObject fire;
    public IEnumerator AcidEffectTick()
    {
        fire.SetActive(true);
        yield return new WaitForSeconds(3.6f);
        fire.SetActive(false);
    }

    public IEnumerator FireEffect()
    {
        yield return null;
    }

    public new void Moving(Vector2 dir)
    {
        transform.Translate(dir * speed * Time.deltaTime);
        var posClamped = transform.position;
        posClamped.y = Mathf.Clamp(transform.position.y, HEIGHT_SCREEN * -1, HEIGHT_SCREEN);
        transform.position = posClamped;
    }



    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        MAX_STATUS_CAR = statusCarSprites.Length - 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            fire.SetActive(true);
            CallDie();
            enabled = false;
        }
        Moving(transform.right);
        MoveKeys();
    }

    private void MoveKeys()
    {
#if UNITY_EDITOR
        // move character for Desktop


        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            Moving(transform.up * -1);
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            Moving(transform.up);
        }


#else
        // android movement and IOS
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector3 pos = Input.GetTouch(0).position;
                pos.z = 8;
                Vector3 realWorldPos = Camera.main.ScreenToWorldPoint(pos);

                if (realWorldPos.y < 0)
                {
                    Moving(transform.up * -1);
                }

                else
                {
                    Moving(transform.up);
                }
            }
        }
#endif
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            Zombie_Racing zombie = collision.GetComponent<Zombie_Racing>();
            killZombie?.Invoke(zombie.Rewardmurder);
            zombie.OnDead();
            if (statusBlood < MAX_STATUS_CAR)
            {
                statusBlood++;
                spriteRenderer.sprite = statusCarSprites[statusBlood];
            }
        }

    }
}
