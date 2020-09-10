using UnityEngine;

public static class LanguageManager
{
    private static Language lang = Language.EN;

    public static Language Language { get => lang; }

    static LanguageManager()
    {
        if (Application.systemLanguage == SystemLanguage.Russian)
        {
            lang = Language.RU;
        }

        else
        {
            lang = Language.EN;
        }
    }

}
