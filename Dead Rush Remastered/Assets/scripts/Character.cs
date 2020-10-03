using System;
using System.Collections;
using UnityEngine;
/// <summary>
/// script of character
/// </summary>
public class Character : ScreenComponent, ICharacter, IHPObject, IDieAudio
{
    [Header("Speed movement value")]
    [SerializeField] protected float speed;
    [Header("entry object spawner bullets")]
    [SerializeField] protected Transform bulletSpawner;
    [Header("skin name")]
    [SerializeField] protected string skinName;
    [Header("fire effect object")]
    [SerializeField] protected GameObject fire_effect;

    [Header("armor value")]
    [SerializeField] protected int armor;

    [Header("health value")]
    [SerializeField] protected int health = 100;

    private SkinCharacter skin;

    private Weapon weapon = new Weapon(40, 3);

    private SpriteRenderer spriteRenderer;

    private StateCharacterType Startedstate = StateCharacterType.gun;

    public int Armor { get => armor; set => armor = value; }
    public int Health { get => health; set => health = value; }
    public Weapon Weapon { get => weapon; }

    public event Action deadEvent;

    [SerializeField] AudioSource ShootClipWeapon;

    // Start is called before the first frame update
    void Start()
    {
        WeaponData data_selected_weapon = GameCache.Player_cacheContainer.selectedWeapon;
        weapon = new Weapon(data_selected_weapon.maxAmmunition, data_selected_weapon.reloadTime);
        Startedstate = (StateCharacterType)Enum.Parse(typeof(StateCharacterType), data_selected_weapon.name_weapon);
        spriteRenderer = GetComponent<SpriteRenderer>();
        skin = new SkinCharacter(skinName);
        SetNewState(Startedstate);
        ShootClipWeapon.clip = Resources.Load<AudioClip>($"fx_shoots/{data_selected_weapon.name_weapon}_shoot");

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
            CallDie();
            PlayAudioDie();
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
            StartCoroutine(((ICharacter)this).FireEffect());
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



    IEnumerator TickReloadWeapon()
    {
        yield return new WaitForSeconds(weapon.reload_time);
        weapon.ReloadWeapon();
        ReturnState();
    }


    public void Damage(int value)
    {
        ShowDamageEffect();
        int value_convert = (value * armor / 100);
        value -= value_convert;
        if (value <= 0)
        {
            value = 1;
        }

        health -= value;
    }

    public void ShowAcidEffect()
    {
        StopCoroutine(((ICharacter)this).AcidEffectTick());
        StartCoroutine(((ICharacter)this).AcidEffectTick());
    }


    IEnumerator ICharacter.FireEffect()
    {
        fire_effect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        ShootClipWeapon.pitch = UnityEngine.Random.Range(0.6f, 2f);
        ShootClipWeapon.PlayOneShot(ShootClipWeapon.clip);
        fire_effect.SetActive(false);
    }

    IEnumerator ICharacter.AcidEffectTick()
    {
        Instantiate(Resources.Load<AudioSource>("fx_prefabs/player_damage_audio"));
        spriteRenderer.color = Color.green;
        yield return new WaitForSeconds(3.6f);
        spriteRenderer.color = Color.white;
    }

    IEnumerator ICharacter.FireEffectTick()
    {
        Instantiate(Resources.Load<AudioSource>("fx_prefabs/player_damage_audio"));
        spriteRenderer.color = Color.gray;
        yield return new WaitForSeconds(3.6f);
        spriteRenderer.color = Color.white;
    }

    public void Moving(Vector2 dir)
    {
        transform.Translate(dir * speed * Time.deltaTime);
        var posClamped = transform.position;
        posClamped.y = Mathf.Clamp(transform.position.y, HEIGHT_SCREEN * -1, HEIGHT_SCREEN);
        transform.position = posClamped;
    }

    protected void CallDie()
    {
        deadEvent?.Invoke();
    }

    public void PlayAudioDie()
    {
        Instantiate(Resources.Load<AudioSource>("fx_prefabs/player_die_audio"));
    }

    public void ShowFireEffect()
    {
        StopCoroutine(((ICharacter)this).FireEffectTick());
        StartCoroutine(((ICharacter)this).FireEffectTick());
    }

    public void ShowDamageEffect()
    {
        StopCoroutine(((ICharacter)this).DamageEffectTick());
        StartCoroutine(((ICharacter)this).DamageEffectTick());
    }

    public IEnumerator DamageEffectTick()
    {
        Instantiate(Resources.Load<AudioSource>("fx_prefabs/player_damage_audio"));
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(3.6f);
        spriteRenderer.color = Color.white;
    }
}
