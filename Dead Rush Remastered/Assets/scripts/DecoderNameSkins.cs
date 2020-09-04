using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

  public static  class DecoderNameSkins
    {
        private static Dictionary<string, string> RUCollection = new Dictionary<string, string>
        {
            {"hitman1", "Хитман" },
             {"womanGreen", "Девушка" },
             {"manBrown", "Мужчина" },
              {"manBlue", "Мужчина" },
              {"manOld", "Пожилой мужчина" },
               {"robot1", "Робот" },
               {"survivor1", "Выживший" },
                {"soldier1", "Солдат" },
        };

        private static Dictionary<string, string> ENCollection = new Dictionary<string, string>
        {
            {"hitman1", "Hitman" },
             {"womanGreen", "Woman" },
             {"manBrown", "Man" },
              {"manBlue", "Man" },
              {"manOld", "Old man" },
               {"robot1", "Robot" },
               {"survivor1", "Survivor" },
                {"soldier1", "Soldier" },
        };

        public static string GetString (string key)
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
