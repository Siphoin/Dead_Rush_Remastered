using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public   class WeaponPanel : MonoBehaviour
    {
    Weapon weapon = null;
    [SerializeField] Text text_ammonutian;
    private void Start()
    {
        weapon = LevelManager.manager.player.Weapon;
        weapon.weaponEvent += EventWeapon;
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

        private void Update()
    {
    }

    private void RefreshText()
    {
        text_ammonutian.text = $"{weapon.ammunition}/{weapon.MaxAmmunition}";
    }
}