using UnityEngine;
using UnityEngine.UI;

public class WeaponPage_Shop : ShopWindowPage
{
    [SerializeField] WeaponData[] weaponsList;
    [SerializeField] Image image_pictogram_weapon;
    [SerializeField] Text weapon_info;
    [SerializeField] Text weapon_name;
    [SerializeField] TransliteText btn_text_action_weapon;
    [SerializeField] GameObject btn_upgrate;
    [SerializeField] Button btn_buy;

    private int selected_index = 0;

    private PageActionType actionType;

    const string PATH_FOLBER_WEAPONS_ICONS = "weapons_icons/";

    const int MAX_LEVEL_WEAPON = 5;
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < weaponsList.Length; i++)
        {
            WeaponData item = weaponsList[i];
            if (GameCache.cacheContainer.weaponsPlayer.ContainsKey(item.name_weapon))
            {
                weaponsList[i] = GameCache.cacheContainer.weaponsPlayer[item.name_weapon];
            }
        }
        ShowWeapon();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextWeapon()
    {
        if (selected_index < weaponsList.Length - 1)
        {
            selected_index++;
            ShowWeapon();
        }
    }

    public void BackWeapon()
    {
        if (selected_index > 0)
        {
            selected_index--;
            ShowWeapon();
        }
    }

    private void ShowWeapon()
    {
        WeaponData target_weapon = weaponsList[selected_index];
        string weaponName = target_weapon.name_weapon;
        Sprite weapon_sprite = Resources.Load<Sprite>($"{PATH_FOLBER_WEAPONS_ICONS}{weaponName}");
        image_pictogram_weapon.sprite = weapon_sprite;
        weapon_name.text = DecoderNameWeapons.GetString(target_weapon.name_weapon);
        if (LanguageManager.Language == Language.EN)
        {
            weapon_info.text = $"Level: {target_weapon.level_upgrate}\nSale: {target_weapon.sale}\nReload time: {target_weapon.reloadTime}  s\nAmmunition: {target_weapon.maxAmmunition}";
        }

        if (LanguageManager.Language == Language.RU)
        {
            weapon_info.text = $"Уровень: {target_weapon.level_upgrate}\nЦена: {target_weapon.sale}\nСкорость перезарядки: {target_weapon.reloadTime}  сек\nБоеприпасы: {target_weapon.maxAmmunition}";
        }
        btn_text_action_weapon.SetupText();
        if (GameCache.cacheContainer.weaponsPlayer.ContainsKey(target_weapon.name_weapon))
        {
            actionType = PageActionType.Select;
            if (LanguageManager.Language == Language.EN)
            {
                btn_text_action_weapon.text = "SELECT";
            }

            if (LanguageManager.Language == Language.RU)
            {
                btn_text_action_weapon.text = "ВЫБРАТЬ";
            }
        }

        else
        {
            actionType = PageActionType.Buy;
        }

        if (actionType == PageActionType.Buy)
        {
            btn_upgrate.SetActive(false);
            btn_buy.interactable = GameCache.cacheContainer.money >= weaponsList[selected_index].sale;
        }

        else
        {
            btn_upgrate.SetActive(target_weapon.level_upgrate != MAX_LEVEL_WEAPON);
            btn_buy.interactable = true;
        }

        if (target_weapon.level_upgrate == MAX_LEVEL_WEAPON)
        {
            if (LanguageManager.Language == Language.EN)
            {
                weapon_info.text = "Max level";
            }

            if (LanguageManager.Language == Language.RU)
            {
                weapon_info.text = "Максимальный уровень";
            }
        }

    }

    public void OnActionWeapon()
    {
        switch (actionType)
        {
            case PageActionType.Select:
                GameCache.cacheContainer.selectedWeapon = weaponsList[selected_index];
                break;
            case PageActionType.Buy:
                GameCache.cacheContainer.weaponsPlayer.Add(weaponsList[selected_index].name_weapon, weaponsList[selected_index]);
                GameCache.cacheContainer.money -= weaponsList[selected_index].sale;
                CallBuyEvent();
                break;
        }

        NewAction();
    }

    private void NewAction()
    {
        GameCache.WritePlayerCache();
        ShowWeapon();
    }

    public void OnUpgrateWeapon()
    {
        GameCache.cacheContainer.money -= weaponsList[selected_index].sale;
        var selected_item = weaponsList[selected_index];
        selected_item.sale *= 2;
        selected_item.level_upgrate++;
        selected_item.maxAmmunition += 25;
        weaponsList[selected_index] = selected_item;
        GameCache.cacheContainer.weaponsPlayer[selected_item.name_weapon] = weaponsList[selected_index];
        NewAction();
        CallBuyEvent();

        if (GameCache.cacheContainer.selectedWeapon.name_weapon == selected_item.name_weapon)
        {
            actionType = PageActionType.Select;
            OnActionWeapon();
        }
    }
}