using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public  class LevelButton : MonoBehaviour
    {
    [SerializeField] TextMeshProUGUI text_level;
  public   int levelIndex { get; set; } = 1;

    private string SceneName;
    [SerializeField] Image[] stars;
    private Sprite star_yes_result_sprite;
    private Sprite block_button_sprite;
    Button button;
    Image image;
    private void Start()
    {

        image = GetComponent<Image>();
        button = GetComponent<Button>();
        text_level.text = levelIndex.ToString();
        SceneName = $"Level{levelIndex}";
        button.onClick.AddListener(new UnityEngine.Events.UnityAction(OnLoadLevel));


    }

    private void OnLoadLevel ()
    {
        Loading.OnLoad(SceneName);
    }

    public void SetDataLevel (LevelIProgressData data)
    {
        star_yes_result_sprite = Resources.Load<Sprite>("UI/star_level_on_result");
        block_button_sprite = Resources.Load<Sprite>("UI/button_level_off");
        for (int i = 0; i < data.starsCount; i++)
        {
            stars[i].sprite = star_yes_result_sprite;
        }
    }

    public void BlockButton ()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            Destroy(stars[i].gameObject);
        }
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        button.interactable = false;
        block_button_sprite = Resources.Load<Sprite>("UI/button_level_off");
        image.sprite = block_button_sprite;
        ColorBlock colorBlock = new ColorBlock();
        colorBlock.disabledColor = Color.white;
        colorBlock.highlightedColor = Color.white;
        colorBlock.normalColor = Color.white;
        colorBlock.pressedColor = Color.white;
        colorBlock.selectedColor = Color.white;
        colorBlock.colorMultiplier = 1;
        button.colors = colorBlock;
    }
}
