using System.Collections.Generic;

public static class DecoderNameWeapons
{
    private static Dictionary<string, string> RUCollection = new Dictionary<string, string>
        {
            {"gun", "Пистолет" },
            {"machine", "Автомат" },
            {"silencer", "Пистолет с глушителем" },
        };

    private static Dictionary<string, string> ENCollection = new Dictionary<string, string>
        {
            {"gun", "Gun" },
            {"machine", "Machine" },
            {"silencer", "Gun Silencer" },
        };

    public static string GetString(string key)
    {
        Language language = LanguageManager.Language;
        if (language == Language.EN)
        {
            return ENCollection[key];
        }

        if (language == Language.RU)
        {
            return RUCollection[key];
        }

        return null;
    }
}
