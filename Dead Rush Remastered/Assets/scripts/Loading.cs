using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Loading : MonoBehaviour
{
    private static string sceneName = "Level";
    [SerializeField] Slider progress;
    [SerializeField] Text hintsText;
    [SerializeField] Image fill_load;
    private bool fill_load_status = false;
    string[] hintsList;
    private float t_text_hints = 0;
    private float t_fill_load = 0;
    private static bool checkCredits = true;
    Color alpha_colorTextHints;
    private Color alpha_fill_load_color = new Color32(255, 255, 255, 125);

    public static string SceneName { get => sceneName; }

    // Use this for initialization
    void Start()
    {
        if (checkCredits)
        {
            if (!GameCache.Player_cacheContainer.backgroundCompleted)
            {

                SceneManager.LoadScene("Background");
            }
        }
        StartCoroutine(FillAnimation());
        var alpha_color = Color.white;
        alpha_color.a = 0;
        alpha_colorTextHints = alpha_color;
        string path_TXT = LanguageManager.Language == Language.EN ? "manifests/hints_en-EN" : "manifests/hints_ru-RU";
        TextAsset mytxtData = (TextAsset)Resources.Load(path_TXT);
        hintsList = mytxtData.text.Split(new string[] { "\n" }, StringSplitOptions.None);
        StartCoroutine(LoadProcessing());
        NewRandomHint();
        StartCoroutine(NewHintDisplay());
        checkCredits = true;
    }

    private void NewRandomHint()
    {
        hintsText.text = hintsList[Random.Range(0, hintsList.Length)];
    }

    // Update is called once per frame

    IEnumerator LoadProcessing()
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

    IEnumerator FillAnimation()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.03f);
            t_fill_load += 0.03f;

            if (!fill_load_status)
            {
                fill_load.color = Color.Lerp(Color.white, alpha_fill_load_color, t_fill_load);
            }

            else
            {
                fill_load.color = Color.Lerp(alpha_fill_load_color, Color.white, t_fill_load);
            }

            if (t_fill_load >= 1)
            {
                fill_load_status = !fill_load_status;
                t_fill_load = 0;
            }
        }
    }

    public static void OnLoad(string SceneName, bool checkCreditsComplete = true)
    {
        checkCredits = checkCreditsComplete;
        sceneName = SceneName;
        SceneManager.LoadScene("Loading");
    }
}
