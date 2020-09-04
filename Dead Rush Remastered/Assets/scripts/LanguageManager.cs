using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

  public static  class LanguageManager
    {
    private static    Language lang = Language.EN;

        public static Language Language { get => lang; }

        static LanguageManager ()
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
