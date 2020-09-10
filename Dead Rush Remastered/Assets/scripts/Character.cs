using Assets.scripts;
using System;
using System.Collections;
using UnityEngine;
/// <summary>
/// script of character
/// </summary>
public class Character : ScreenComponent
{
    [Header("Speed movement value")]
    [SerializeField] float speed;
    [Header("entry object spawner bullets")]
    [SerializeField] Transform bulletSpawner;
    [Header("skin name")]
    [SerializeField] string skinName;
    [Header("fire effect object")]
    [SerializeField] GameObject fire_effect;

    [Header("armor value")]
    [SerializeField] int armor;

    [Header("health value")]
    [SerializeField] int health = 100;

    private SkinCharacter skin;

    private Weapon weapon = new Weapon(40, 3);

    private SpriteRenderer spriteRenderer;

    private StateCharacterType Startedstate = StateCharacterType.gun;

    public int Armor { get => armor; set => armor = value; }
    public int Health { get => health; set => health = value; }
    public Weapon Weapon { get => weapon; }

    public event Action deadEvent;

    // Start is called before the first frame update
    void Start()
    {
        WeaponData data_selected_weapon = GameCache.cacheContainer.selectedWeapon;
        weapon = new Weapon(data_selected_weapon.maxAmmunition, data_selected_weapon.reloadTime);
        Startedstate = (StateCharacterType)Enum.Parse(typeof(StateCharacterType), data_selected_weapon.name_weapon);
        spriteRenderer = GetComponent<SpriteRenderer>();
        skin = new SkinCharacter(skinName);
        SetNewState(Startedstate);

    }

    private void SetNewState(StateCharacterType s)
    {
        spriteRenderer.sprite = skin.GetSkinState(s);
    }

    private void ReturnState()
    {
        spriteRenderer.sprite = skin.GetSkinState(Startedstate);
    }

    // Update is called once per frame
    void Update()
    {
        MoveKeys();

        if (health <= 0)
        {
            GameObject blood = Instantiate(Resources.Load<GameObject>("Prefabs/blood"));
            blood.transform.position = transform.position;
            deadEvent();
            Destroy(gameObject);
        }
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            RefreshWeapon();
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

    public void OnFire()
    {
        if (weapon.reloading)
        {
            return;
        }

        if (weapon.ammunition > 0)
        {
            GameObject bullet = Instantiate(Resources.Load<GameObject>("Prefabs/bullet"));
            bullet.transform.position = bulletSpawner.position;
            weapon.ammunition--;
            StartCoroutine(FireEffect());
        }






        if (weapon.ammunition <= 0)
        {
            RefreshWeapon();
        }

    }

    public void RefreshWeapon()
    {
        if (weapon.reloading)
        {
            return;
        }
        weapon.reloading = true;
        SetNewState(StateCharacterType.reload);
        StartCoroutine(TickReloadWeapon());

    }

    private void Moving(Vector2 dir)
    {
        transform.Translate(dir * speed * Time.deltaTime);
        var posClamped = transform.position;
        posClamped.y = Mathf.Clamp(transform.position.y, HEIGHT_SCREEN * -1, HEIGHT_SCREEN);
        transform.position = posClamped;
    }


    IEnumerator TickReloadWeapon()
    {
        yield return new WaitForSeconds(weapon.reload_time);
        weapon.ReloadWeapon();
        ReturnState();
    }

    IEnumerator FireEffect()
    {
        fire_effect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        fire_effect.SetActive(false);
    }

    public void Damage(int value)
    {
        int value_convert = (value * armor / 100);
        value -= value_convert;
        if (value < 0)
        {
            value = 1;
        }

        health -= value;
    }

    public void ShowAcidEffect()
    {
        StopCoroutine(AcidEffectTick());
        StartCoroutine(AcidEffectTick());
    }

    IEnumerator AcidEffectTick()
    {
        spriteRenderer.color = Color.green;
        yield return new WaitForSeconds(3.6f);
        spriteRenderer.color = Color.white;
    }
}
