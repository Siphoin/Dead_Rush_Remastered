using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Background : MonoBehaviour
{
    [SerializeField] Text Text_background;
         private   string[] textList;
    private float t_text_hints = 0;
    Color alpha_colorText;
    private int index_text = 0;
    // Use this for initialization
    void Start()
    {
        var alpha_color = Color.white;
        alpha_color.a = 0;
        alpha_colorText = alpha_color;
        StartCoroutine(NewTextDisplay());
        string data = null;
        switch (LanguageManager.Language)
        {
            case Language.EN:
                data = Resources.Load<TextAsset>("manifests/TextBackground_en-EN").text;
                break;

            case Language.RU:
                data = Resources.Load<TextAsset>("manifests/TextBackground_ru-RU").text;
                break;
        }
    textList =    data.Split(new string[] { "\n" }, StringSplitOptions.None);
        Text_background.text = textList[index_text];
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator NewTextDisplay()
    {
        while (true)
        {
            float time = 0;
            switch (index_text)
            {

                case 0:
                    time = 4;

                    break;
                case 1:
                    time = 7;
                    break;

                case 2:
                    time = 2;
                    break;

                case 3:
                    time = 0.6f;
                    break;

                case 4:
                    time = 0.6f;
                    break;

                case 5:
                    time = 0.6f;
                    break;

                case 6:
                    time = 0.6f;
                    break;

                case 7:
                    time = 1.6f;
                    break;

                case 8:
                    time = 3f;
                    break;

                case 9:
                    time = 13f;
                    break;

                case 10:
                    time = 10f;
                    break;

                case 11:
                    time = 1f;
                    break;
            }
            yield return new WaitForSeconds(time);
            t_text_hints = 0;
            index_text++;
            Text_background.color = alpha_colorText;
            Text_background.text = textList[index_text];
            while (Text_background.color.a != 1)
            {
                yield return new WaitForSeconds(0.01f);
                t_text_hints += 0.01f;
                Text_background.color = Color.Lerp(alpha_colorText, Color.white, t_text_hints);
            }
            if (index_text == textList.Length - 1)
            {
                GameCache.cacheContainer.backgroundCompleted = true;
                GameCache.WritePlayerCache();
                yield return new WaitForSeconds(3);
                string sceneTarget = Loading.SceneName;
                Loading.OnLoad(sceneTarget);
                yield break;
            }

        }
    }

    
}
