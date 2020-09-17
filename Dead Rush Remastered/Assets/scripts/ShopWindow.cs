using UnityEngine;
using UnityEngine.UI;

public class ShopWindow : Window
{
    [SerializeField] Text text_money_value;
    private Vector3 starterPositionBorderSelector;
    private Vector3 movePositionBorder;
    private Vector3 movePositionBorder_baricades;

    private Vector3 point_target_border = Vector3.zero;


    [SerializeField] private Image image;
    [SerializeField] RectTransform border;
    [SerializeField] RectTransform border_partners_point;
    [SerializeField] RectTransform border_baricades_point;

    [SerializeField] WeaponPage_Shop weaponPage;
    [SerializeField] BaricadesPage_Shop barricadesPage;
    [SerializeField] PartnersPage_Shop partnersPage_Shop;

    private ShopPage selected_page = ShopPage.Weapons;

    private float t_border = 0;

    // Use this for initialization
    void Start()
    {

        ShowPage();
        starterPositionBorderSelector = border.position;
        movePositionBorder = border_partners_point.position;
        movePositionBorder_baricades = border_baricades_point.position;
        DisplayValueMoney();
        weaponPage.buyEvent += DisplayValueMoney;
        barricadesPage.buyEvent += DisplayValueMoney;
        partnersPage_Shop.buyEvent += DisplayValueMoney;
    }

    private void DisplayValueMoney()
    {
        text_money_value.text = $"${GameCache.player_cacheContainer.money}";
    }

    // Update is called once per frame
    void Update()
    {
        if (t_border < 1)
        {
            if (point_target_border != Vector3.zero)
            {
                t_border += 0.03f;
                border.position = Vector3.Lerp(border.position, point_target_border, t_border);
            }

        }
    }

    public void SelectWeapons()
    {
        if (selected_page == ShopPage.Weapons)
        {
            return;
        }
        selected_page = ShopPage.Weapons;
        NewTargetBorder(starterPositionBorderSelector);
        ShowPage();

    }

    private void NewTargetBorder(Vector3 target)
    {
        point_target_border = target;
        t_border = 0;
    }

    public void SelectPartners()
    {
        if (selected_page == ShopPage.Partners)
        {
            return;
        }
        NewTargetBorder(movePositionBorder);
        selected_page = ShopPage.Partners;
        ShowPage();
    }

    public void SelectBaricades()
    {
        if (selected_page == ShopPage.Baricades)
        {
            return;
        }
        NewTargetBorder(movePositionBorder_baricades);
        selected_page = ShopPage.Baricades;
        ShowPage();
    }

    private void ShowPage()
    {
        switch (selected_page)
        {
            case ShopPage.Weapons:
                SetStateWeaponsPage(true);
                SetStateBaricadesPage(false);
                SetStatePartnersPage(false);
                break;
            case ShopPage.Partners:
                SetStateWeaponsPage(false);
                SetStateBaricadesPage(false);
                SetStatePartnersPage(true);
                break;

            case ShopPage.Baricades:
                SetStateBaricadesPage(true);
                SetStateWeaponsPage(false);
                SetStatePartnersPage(false);
                break;
        }
    }

    private void SetStateWeaponsPage(bool state)
    {
        weaponPage.gameObject.SetActive(state);
    }

    private void SetStateBaricadesPage(bool state)
    {
        barricadesPage.gameObject.SetActive(state);
    }

    private void SetStatePartnersPage(bool state)
    {
        partnersPage_Shop.gameObject.SetActive(state);
    }
}