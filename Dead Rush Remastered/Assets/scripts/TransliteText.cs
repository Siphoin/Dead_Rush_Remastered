using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public  class TransliteText : MonoBehaviour
{
    [Header("data")]
    [SerializeField] TransliteString data = new TransliteString();

    protected Text text_original;
    protected TextMeshProUGUI text_tmp;
    private void Awake()
    {
        SetupText();
    }

    public void SetupText(string argRU = "", string argEN = "")
    {
        if (TryGetComponent(out text_tmp))
        {
            text_tmp.text =  LanguageManager.Language == Language.EN ? data.string_en_EN +argEN :  data.string_ru_RU + argRU ;

        }

        if (TryGetComponent(out text_original))
        {
            text_original.text = LanguageManager.Language == Language.EN ? data.string_en_EN + argEN : data.string_ru_RU + argRU;

        }
    }

    public void SetupText()
    {
        if (TryGetComponent(out text_tmp))
        {
            text_tmp.text = LanguageManager.Language == Language.EN ?  data.string_en_EN :  data.string_ru_RU;

        }

        if (TryGetComponent(out text_original))
        {
            text_original.text = LanguageManager.Language == Language.EN ?  data.string_en_EN : data.string_ru_RU;

        }
    }
}
