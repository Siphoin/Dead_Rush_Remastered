using UnityEngine;
using UnityEngine.UI;

public class BaricadesPage_Shop : ShopWindowPage
{
    private BaricadeData[] baricadesList;
    [SerializeField] Image image_pictogram_baricades;
    [SerializeField] Text baricades_info;
    [SerializeField] Text baricades_name;
    [SerializeField] TransliteText btn_text_action_baricades;
    [SerializeField] Button btn_buy;
    private int selected_index = 0;

    private PageActionType actionType;

    const string PATH_FOLBER_BARICADES_ICONS = "baricades_icons/";

    const int MAX_LEVEL_BARICADES = 5;
    // Use this for initialization
    void Start()
    {
        Baricade[] baricadesObjects = Resources.LoadAll<Baricade>("Prefabs/Baricades");
        baricadesList = new BaricadeData[baricadesObjects.Length];
        for (int i = 0; i < baricadesList.Length; i++)
        {
            baricadesList[i] = baricadesObjects[i].GetData();
        }
        ShowBaricades();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowBaricades()
    {
        BaricadeData target_baricades = baricadesList[selected_index];
        string baricadesName = target_baricades.name_prefab;
        Sprite baricades_sprite = Resources.Load<Sprite>($"{PATH_FOLBER_BARICADES_ICONS}{baricadesName}");
        image_pictogram_baricades.sprite = baricades_sprite;
        baricades_name.text = DecoderBaricadesNames.GetString(target_baricades.name_prefab);
        if (LanguageManager.Language == Language.EN)
        {
            baricades_info.text = $"Armor: {target_baricades.armor}\nHealth: {target_baricades.health}\nSale: {target_baricades.sale}";
        }

        if (LanguageManager.Language == Language.RU)
        {
            baricades_info.text = $"Прочность: {target_baricades.armor}\nЗдоровье: {target_baricades.health}\nЦена: {target_baricades.sale}";
        }
        btn_text_action_baricades.SetupText();
        if (GameCache.Player_cacheContainer.baricadesPlayer.ContainsKey(target_baricades.name_prefab))
        {
            actionType = PageActionType.Select;
            if (LanguageManager.Language == Language.EN)
            {
                btn_text_action_baricades.text = "SETUP";
            }

            if (LanguageManager.Language == Language.RU)
            {
                btn_text_action_baricades.text = "УСТАНОВИТЬ";
            }

            btn_buy.interactable = true;


        }

        else
        {
            btn_buy.interactable = GameCache.Player_cacheContainer.money >= baricadesList[selected_index].sale;
            actionType = PageActionType.Buy;
        }

    }


    public void NextBaricades()
    {
        if (selected_index < baricadesList.Length - 1)
        {
            selected_index++;
            ShowBaricades();
        }
    }

    public void BackBaricades()
    {
        if (selected_index > 0)
        {
            selected_index--;
            ShowBaricades();
        }
    }

    public void OnActionBaricades()
    {
        switch (actionType)
        {
            case PageActionType.Select:
                GameCache.Player_cacheContainer.baricades = baricadesList[selected_index];
                break;
            case PageActionType.Buy:
                GameCache.Player_cacheContainer.baricadesPlayer.Add(baricadesList[selected_index].name_prefab, baricadesList[selected_index]);
                GameCache.Player_cacheContainer.money -= baricadesList[selected_index].sale;
                CallBuyEvent();
                break;
        }

        NewAction();
    }

    private void NewAction()
    {
        GameCache.SaveData();
        ShowBaricades();
    }
}
