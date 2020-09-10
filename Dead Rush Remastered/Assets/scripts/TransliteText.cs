using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransliteText : MonoBehaviour
{
    [Header("data")]
    [SerializeField] TransliteString data = new TransliteString();

    protected Text text_original;
    protected TextMeshProUGUI text_tmp;
    public string text
    {

        get
        {
            if (text_original != null)
            {
                return text_original.text;
            }

            if (text_tmp != null)
            {
                return text_tmp.text;
            }

            return null;
        }

        set
        {
            if (text_original != null)
            {
                text_original.text = value;
            }

            if (text_tmp != null)
            {
                text_tmp.text = value;
            }


        }

    }
    private void Awake()
    {
        SetupText();
    }

    public void SetupText(string argRU = "", string argEN = "")
    {
        if (TryGetComponent(out text_tmp))
        {
            text_tmp.text = LanguageManager.Language == Language.EN ? data.string_en_EN + argEN : data.string_ru_RU + argRU;

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
            text_tmp.text = LanguageManager.Language == Language.EN ? data.string_en_EN : data.string_ru_RU;

        }

        if (TryGetComponent(out text_original))
        {
            text_original.text = LanguageManager.Language == Language.EN ? data.string_en_EN : data.string_ru_RU;

        }
    }



}
