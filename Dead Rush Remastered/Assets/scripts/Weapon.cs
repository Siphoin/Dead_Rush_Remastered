
using System;

public  class Weapon
    {
        int Ammunition = 0;
        int maxAmmunition = 0;
    bool Reloading = false;
    private float ReloadTime = 0;

    public event Action<WeaponState> weaponEvent;
    public int ammunition
    {
        get => Ammunition; set
        {
            Ammunition = value;
            weaponEvent?.Invoke(WeaponState.NewValue);
        }
    }
        public int MaxAmmunition { get => maxAmmunition; }
    public bool reloading { get => Reloading; set => Reloading = value; }
    public float reload_time { get => ReloadTime; }

    public Weapon (int ammunition_max, float time_reload)
        {
            maxAmmunition = ammunition_max;;
        Ammunition = maxAmmunition;
        ReloadTime = time_reload;
    }

    public void ReloadWeapon ()
    {
        Reloading = false;
        Ammunition = maxAmmunition;
        weaponEvent?.Invoke(WeaponState.NewValue);
    }
    }
