using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class Loading : MonoBehaviour
{
    private static string sceneName = "Level";
    [SerializeField] Slider progress;
    [SerializeField] Text hintsText;
    string[] hintsList;
    private float t_text_hints = 0;
    Color alpha_colorTextHints;
    // Use this for initialization
    void Start()
    {
        var alpha_color = Color.white;
        alpha_color.a = 0;
        alpha_colorTextHints = alpha_color;
     string path_TXT =   LanguageManager.Language == Language.EN ? "manifests/hints_en-EN" : "manifests/hints_ru-RU";
        TextAsset mytxtData = (TextAsset)Resources.Load(path_TXT);
        hintsList = mytxtData.text.Split(new string[] { "\n" }, StringSplitOptions.None);
       StartCoroutine(LoadProcessing());
        NewRandomHint();
        StartCoroutine(NewHintDisplay());
    }

    private void NewRandomHint()
    {
        hintsText.text = hintsList[Random.Range(0, hintsList.Length)];
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator LoadProcessing ()
    {
        progress.value = 0;
        yield return new WaitForSeconds(3);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncOperation.isDone)
        {
            progress.value = asyncOperation.progress;
            yield return null;
        }
    }

    IEnumerator NewHintDisplay()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            NewRandomHint();
            t_text_hints = 0;
            hintsText.color = alpha_colorTextHints;
           while (hintsText.color.a != 1)
            {
                yield return new WaitForSeconds(0.01f);
                t_text_hints += 0.01f;
                hintsText.color = Color.Lerp(alpha_colorTextHints, Color.white, t_text_hints);
            }
        }
    }

    public static void OnLoad (string SceneName)
    {
        sceneName = SceneName;
        SceneManager.LoadScene("Loading");
    }
}
