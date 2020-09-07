using UnityEngine;
using System.Collections;
using System.IO;
using System;
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

    public static bool OnPageLevels { get => onPageLevels; set => onPageLevels = value; }


    // Use this for initialization
    void Start()
    {
        if (!onLoadSave)
        {
            GameCache.ReadPlayerData();
            onLoadSave = true;
        }
        if (onPageLevels)
        {
            OnPlay();
            onPageLevels = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        button_shop.interactable = GameCache.FileSaveExits();
    }

    public void OnPlay ()
    {

        StartCoroutine(CheckPlay());
    }

    public void OnBack ()
    {
        animator_menu.Play(onBackAnimationName);
    }

    public void OnShop ()
    {
          Instantiate(Resources.Load<ShopWindow>("Prefabs/ShopWindow"));
    }

    private IEnumerator CheckPlay ()
    {
        if (!GameCache.FileSaveExits())
        {
            Window newZombieWindow = Instantiate(Resources.Load<Window>("Prefabs/WindowCreationProfilePlayer"));
            newZombieWindow.exitEvent += OnWindowCreationExit;
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
