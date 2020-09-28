using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] Animation animator_menu;
    const string onPlayAnimationName = "menuPlay";
    const string onBackAnimationName = "menuBack";
    private static bool onPageLevels = false;
    private static bool onLoadSave = false;
    private bool windowCreationExited;
    [SerializeField] Button button_shop;
    [SerializeField] Button survival_shop;
    [SerializeField] TransliteText completed_text;

    public static bool OnPageLevels { get => onPageLevels; set => onPageLevels = value; }


    // Use this for initialization
    void Start()
    {
        if (!onLoadSave)
        {
            onLoadSave = true;
        }
        if (onPageLevels)
        {
            OnPlay();
            onPageLevels = false;
        }

        survival_shop.interactable = GameCache.GameFinished();
        float completedValue = (float)GameCache.Player_cacheContainer.levelCompleted / LevelManager.MAX_LEVEL_GAME * 100;
        string completedText = $"{completedValue}%";
        completed_text.SetupText(completedText, completedText);
    }

    // Update is called once per frame
    void Update()
    {
        button_shop.interactable = GameCache.FileSaveExits();
    }

    public void OnPlay()
    {

        StartCoroutine(CheckPlay());
    }

    public void OnBack()
    {
        animator_menu.Play(onBackAnimationName);
    }

    public void OnShop()
    {
        Instantiate(Resources.Load<ShopWindow>("Prefabs/ShopWindow"));
    }

    public void OnSettings ()
    {
        Instantiate(Resources.Load<SettingsWindow>("Prefabs/SettingsWindow"));
    }

    public void OnSurvival ()
    {
        Loading.OnLoad("Survival");
    }

    private IEnumerator CheckPlay()
    {
        if (!GameCache.FileSaveExits())
        {
            Window windowCreateProfile = Instantiate(Resources.Load<Window>("Prefabs/WindowCreationProfilePlayer"));
            windowCreateProfile.exitEvent += OnWindowCreationExit;
            while (windowCreationExited == false)
            {
                yield return new WaitForSeconds(1 / 60);
            }
        }
        animator_menu.Play(onPlayAnimationName);
    }

    private void OnWindowCreationExit()
    {
        windowCreationExited = true;
    }
}
