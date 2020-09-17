using UnityEngine;
using UnityEngine.UI;

public class PartnersPage_Shop : ShopWindowPage
{
    [SerializeField] Button button_buy;
    [SerializeField] int sale;
    [SerializeField] Text text_info;
    // Use this for initialization
    void Start()
    {
        switch (LanguageManager.Language)
        {
            case Language.EN:
                text_info.text = $"Sale: {sale}\nWeapon: Gun\nAmmunition: 40";
                break;
            case Language.RU:
                text_info.text = $"Цена: {sale}\nОружие: Пистолет\nБоеприпасы: 40";
                break;
        }
        button_buy.interactable = GameCache.player_cacheContainer.money >= sale;
        if (GameCache.player_cacheContainer.partnerBuyed)
        {
            Destroy(button_buy.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuyPartner()
    {
        GameCache.player_cacheContainer.money -= sale;
        GameCache.player_cacheContainer.partnerBuyed = true;
        Destroy(button_buy.gameObject);
        CallBuyEvent();
        GameCache.SaveData();
    }
}