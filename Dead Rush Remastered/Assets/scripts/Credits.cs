using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour
{
    [SerializeField] private GameObject credits_container;
    [SerializeField] private float speed_translate;
    [SerializeField] private UnityEngine.UI.Text text_credits;
    const string credits_file_name = "credits_";
    const int Y_CHECK_POS_CREDITS = 7568;
    // Use this for initialization
    void Start()
    {
        BlackTransitionCaller.Create();
        string pathToFile = credits_file_name;
        switch (LanguageManager.Language)
        {
            case Language.EN:
                pathToFile += "en-EN";
                break;
            case Language.RU:
                pathToFile += "ru-RU";
                break;
        }

        text_credits.text = Resources.Load<TextAsset>($"manifests/{pathToFile}").text;

    }

        // Update is called once per frame
        void Update()
    {
        credits_container.transform.Translate(credits_container.transform.up * speed_translate * Time.deltaTime);

        if (credits_container.transform.position.y >= Y_CHECK_POS_CREDITS)
        {
            BackToMenu();
        }

#if UNITY_EDITOR
if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            BackToMenu();
        }
#else

if (Input.touchCount > 0)
        {
            BackToMenu();
        }
#endif
    }

    private  void BackToMenu()
    {
        Menu.OnPageLevels = false;
        Loading.OnLoad("Menu");
    }
}