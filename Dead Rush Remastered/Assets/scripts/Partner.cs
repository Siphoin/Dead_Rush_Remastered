using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Partner : ScreenComponent, ICharacter, IHPObject, IDieAudio
{
    [Header("Speed movement value")]
    [SerializeField] float speed;
    [Header("entry object spawner bullets")]
    [SerializeField] Transform bulletSpawner;
    private string skinName;
    [Header("fire effect object")]
    [SerializeField] GameObject fire_effect;

    [Header("armor value")]
    [SerializeField] int armor;

    [Header("health value")]
    [SerializeField] int health = 100;

    private SkinCharacter skin;

    private Weapon weapon = new Weapon(40, 2);

    private SpriteRenderer spriteRenderer;

    private StateCharacterType Startedstate = StateCharacterType.gun;
    [SerializeField] AudioSource ShootClipWeapon;
    [SerializeField] Slider HP_Slider;

    private string[] skinsArray = new string[]
    {
        "hitman1",
        "soldier1",
        "robot1",
        "womanGreen",
        "manOld",
        "manBlue",
        "manBrown",
        "survivor1",
    };

    public event Action deadEvent;


    public int Armor { get => armor; set => armor = value; }
    public int Health { get => health; set => health = value; }
    public Weapon Weapon { get => weapon; }



    private PartnerVisibleComponent visibleComponent;
    // Use this for initialization
    void Start()
    {

        skinName = skinsArray[Random.Range(0, skinsArray.Length)];
        skinsArray = null;
        visibleComponent = GameObject.FindGameObjectWithTag("PartnerVisibleComponent").GetComponent<PartnerVisibleComponent>();
        StartCoroutine(OnFire());
        spriteRenderer = GetComponent<SpriteRenderer>();
        skin = new SkinCharacter(skinName);
        SetNewState(Startedstate);
        var posPartner = transform.position;
        posPartner.y = Random.Range(HEIGHT_SCREEN * -1, HEIGHT_SCREEN);
        transform.position = posPartner;
        LevelManager.manager.HordeEvent += HordeOn;
        ShootClipWeapon.clip = Resources.Load<AudioClip>("fx_shoots/gun_shoot");
        HP_Slider.maxValue = health;
    }

    private void HordeOn()
    {
        RefreshWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (visibleComponent.ZombiesEntered)
        {
            ZombieBase zombie = visibleComponent.End_zombie;
            MovingLogic(zombie);
        }

        if (health <= 0)
        {
            GameObject blood = Instantiate(Resources.Load<GameObject>("Prefabs/blood"));
            blood.transform.position = transform.position;
            PlayAudioDie();
            deadEvent?.Invoke();
            Destroy(gameObject);
        }

        HP_Slider.value = health;
    }

    private void MovingLogic(ZombieBase zombie)
    {
        if (zombie.transform.position.y > transform.position.y)
        {
            Moving(transform.up);
        }

        if (zombie.transform.position.y < transform.position.y)
        {
            Moving(transform.up * -1);
        }

    }

    public void Moving(Vector2 dir)
    {
        transform.Translate(dir * speed * Time.deltaTime);
        var posClamped = transform.position;
        posClamped.y = Mathf.Clamp(transform.position.y, HEIGHT_SCREEN * -1, HEIGHT_SCREEN);
        transform.position = posClamped;
    }

    IEnumerator OnFire()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / 60);
            if (weapon.reloading)
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.4f);
            if (weapon.ammunition > 0 && visibleComponent.ZombiesEntered)
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

    IEnumerator ICharacter.FireEffect()
    {
        fire_effect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        ShootClipWeapon.pitch = Random.Range(0.6f, 2f);
        ShootClipWeapon.PlayOneShot(ShootClipWeapon.clip);
        fire_effect.SetActive(false);
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

    private void SetNewState(StateCharacterType s)
    {
        spriteRenderer.sprite = skin.GetSkinState(s);
    }

    private void ReturnState()
    {
        spriteRenderer.sprite = skin.GetSkinState(Startedstate);
    }

    public void ShowAcidEffect()
    {
        StopCoroutine(((ICharacter)this).AcidEffectTick());
        StartCoroutine(((ICharacter)this).AcidEffectTick());
    }


    void ICharacter.OnFire()
    {
        // nothing
    }


    IEnumerator ICharacter.AcidEffectTick()
    {
        Instantiate(Resources.Load<AudioSource>("fx_prefabs/partner_damage_audio"));
        spriteRenderer.color = Color.green;
        yield return new WaitForSeconds(3.6f);
        spriteRenderer.color = Color.white;
    }

    public void PlayAudioDie()
    {
        Instantiate(Resources.Load<AudioSource>("fx_prefabs/partner_die_audio"));
    }

    public void ShowFireEffect()
    {
        StopCoroutine(((ICharacter)this).FireEffectTick());
        StartCoroutine(((ICharacter)this).FireEffectTick());
    }

    public IEnumerator FireEffectTick()
    {
        Instantiate(Resources.Load<AudioSource>("fx_prefabs/player_damage_audio"));
        spriteRenderer.color = Color.gray;
        yield return new WaitForSeconds(3.6f);
        spriteRenderer.color = Color.white;
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
