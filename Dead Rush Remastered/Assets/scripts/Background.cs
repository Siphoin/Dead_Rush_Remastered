using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField] Text Text_background;
    private string[] textList;
    private float t_text_hints = 0;
    Color alpha_colorText;
    private int index_text = 0;

    private float[] timesKeys = new float[]
    {
        4,
        7,
        2,
        0.6f,
        0.6f,
        0.6f,
        0.6f,
        1.6f,
        3f,
        13f,
        10f,
        1f,
    };
    // Use this for initialization
    void Start()
    {
        BlackTransitionCaller.Create();
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
        textList = data.Split(new string[] { "\n" }, StringSplitOptions.None);
        Text_background.text = textList[index_text];
    }

    IEnumerator NewTextDisplay()
    {
        while (true)
        {
            yield return new WaitForSeconds(timesKeys[index_text]);
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
                GameCache.Player_cacheContainer.backgroundCompleted = true;
                GameCache.SaveData();
                yield return new WaitForSeconds(3);
                string sceneTarget = Loading.SceneName;
                Loading.OnLoad(sceneTarget);
                yield break;
            }

        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            SkipCredits();
        }



#else
if (Input.touchCount > 0)
        {
            SkipCredits();
        }
#endif
    }

    private void SkipCredits()
    {
        GameCache.Player_cacheContainer.backgroundCompleted = true;
        GameCache.SaveData();
        string sceneTarget = Loading.SceneName;
        Loading.OnLoad(sceneTarget);
    }
}
