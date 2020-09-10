using System.Collections.Generic;

public static class DecoderBaricadesNames
{
    private static Dictionary<string, string> RUCollection = new Dictionary<string, string>
        {
            {"baricades1", "Коробки" },
            {"baricades2", "Коробки | Уровень 2" },
            {"baricades3", "Коробки | Уровень 3" },
            {"baricades4", "Загрождение" },
            {"baricades5", "Загрождение | Уровень 2" },
            {"baricades6", "Загрождение | Уровень 3" },
        };

    private static Dictionary<string, string> ENCollection = new Dictionary<string, string>
        {
            {"baricades1", "Boxes" },
            {"baricades2", "Boxes | Level 2" },
            {"baricades3", "Boxes | Level 3" },
            {"baricades4", "Obstruction" },
            {"baricades5", "Obstruction | Level 2" },
            {"baricades6", "Obstruction | Level 3" },
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
