using UnityEngine;
using System.Collections;
using Assets.scripts;
using System;

public class DialogPublisher : MonoBehaviour
{
    [SerializeField] GameObject triagle;
    [SerializeField] bool isPlayer = false;
    private SpriteRenderer spriteRenderer;
    private SkinCharacter skin;
    private StateCharacterType Startedstate;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (isPlayer)
        {
            WeaponData data_selected_weapon = GameCache.cacheContainer.selectedWeapon;
            Startedstate = (StateCharacterType)Enum.Parse(typeof(StateCharacterType), data_selected_weapon.name_weapon);
            skin = new SkinCharacter(GameCache.cacheContainer.skin);
            spriteRenderer.sprite = skin.GetSkinState(Startedstate);
        }
        SetStatusTalk(false);
    }

    public void SetStatusTalk (bool status)
    {
        triagle.SetActive(status);
    }
}
