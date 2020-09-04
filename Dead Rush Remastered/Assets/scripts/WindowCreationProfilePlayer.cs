using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using TMPro;
public class WindowCreationProfilePlayer : Window
{
 private   ItemSkinSelector[] items_skins;
    [SerializeField] Image image_skin_display;
    [SerializeField] Text text_name_skin;
    [SerializeField] TMP_InputField inputField;

    int index_skin = 0;
 [SerializeField]   private GameObject window_anim;

    // Use this for initialization
    void Start()
    {
        target_window = window_anim;
        StartAnimation();
        SpriteRenderer[] charactersList = Resources.LoadAll<SpriteRenderer>("Prefabs/Characters");
        items_skins = new ItemSkinSelector[charactersList.Length];
        for (int i = 0; i < charactersList.Length; i++)
        {
            items_skins[i] = new ItemSkinSelector(charactersList[i].sprite, charactersList[i].gameObject.name);
        }

        ChangeSkin();
    }

    // Update is called once per frame
    void Update()
    {
        AnimationProcess();
    }

public void NextIndex ()
    {
        if (index_skin < items_skins.Length - 1)
        {
            index_skin++;
            ChangeSkin();
        }


    }

    public void BackIndex()
    {
        if (index_skin > 0)
        {
            index_skin--;
            ChangeSkin();
        }


    }

    public void Select ()
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            return;
        }
        inputField.text = inputField.text.Trim();
        GameCache.cacheContainer = new GameCacheContainer();
        GameCache.cacheContainer.namePlayer = inputField.text;
        GameCache.cacheContainer.skin = items_skins[index_skin].NameSkin;
        GameCache.WritePlayerCache();
        Exit();
    }

    private void ChangeSkin ()
    {
        text_name_skin.text = DecoderNameSkins.GetString(items_skins[index_skin].NameSkin);
        image_skin_display.sprite = items_skins[index_skin].SkinSprite;
    }
}
