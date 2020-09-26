using UnityEngine;
using UnityEngine.UI;

public class WeaponPanel : MonoBehaviour
{
    Weapon weapon = null;
    [SerializeField] Text text_ammonutian;
    private Image weapon_ico;
    const string PATH_FOLBER_WEAPONS_ICONS = "weapons_icons/";
    private void Start()
    {
        weapon_ico = GetComponent<Image>();
        weapon = LevelManager.manager.player.Weapon;
        weapon.weaponEvent += EventWeapon;
        weapon_ico.sprite = Resources.Load<Sprite>($"{PATH_FOLBER_WEAPONS_ICONS}{GameCache.Player_cacheContainer.selectedWeapon.name_weapon}");
        RefreshText();
    }

    private void EventWeapon(WeaponState state)
    {

        switch (state)
        {
            case WeaponState.WeaponChanged:

                break;
            case WeaponState.NewValue:
                RefreshText();
                break;
        }
    }


    private void RefreshText()
    {
        text_ammonutian.text = $"{weapon.ammunition}/{weapon.MaxAmmunition}";
    }
}