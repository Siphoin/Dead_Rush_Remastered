using System;
using System.Collections;
using UnityEngine;

public class DialogPublisher : MonoBehaviour
{
    [SerializeField] RectTransform triagle;
    [SerializeField] bool isPlayer = false;
    private SpriteRenderer spriteRenderer;
    private SkinCharacter skin;
    private StateCharacterType Startedstate;

    [SerializeField] Transform bulletSpawner;
    [Header("skin name")]
    [SerializeField] string skinName;
    [Header("fire effect object")]
    [SerializeField] GameObject fire_effect;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (isPlayer)
        {
            WeaponData data_selected_weapon = GameCache.player_cacheContainer.selectedWeapon;
            Startedstate = (StateCharacterType)Enum.Parse(typeof(StateCharacterType), data_selected_weapon.name_weapon);
            skin = new SkinCharacter(GameCache.player_cacheContainer.skin);
            spriteRenderer.sprite = skin.GetSkinState(Startedstate);
        }
        SetStatusTalk(false);
    }

    public void SetStatusTalk(bool status)
    {
        if (triagle == null)
        {
            return;
        }
        triagle.gameObject.SetActive(status);
    }


    public BulletCinematic OnFire(DialogPublisher target = null)
    {
      BulletCinematic bullet = Instantiate(Resources.Load<BulletCinematic>("cinematic/bulletCinematic"));
        bullet.transform.position = bulletSpawner.position;;
        if (target != null)
        {
            bullet.SetTarget(target);
        }
        StartCoroutine(FireEffect());
        return bullet;
    }

    IEnumerator FireEffect()
    {
        fire_effect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        fire_effect.SetActive(false);
    }

    public void OnDead()
    {
        GameObject blood = Instantiate(Resources.Load<GameObject>("Prefabs/blood"));
        blood.transform.position = transform.position;
        Destroy(gameObject);
    }
}
