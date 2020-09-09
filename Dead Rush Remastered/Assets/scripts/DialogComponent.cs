using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class DialogComponent : MonoBehaviour
{
    [SerializeField] private DialogElement[] dialogElements = new DialogElement[0];

  private  Dictionary<string, DialogPublisher> targets = new Dictionary<string, DialogPublisher>();

  private Text text_dialog;

    private int indexDialog = -1;

    private char[] currentChars;

    public static string LevelName { get; set; }
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < dialogElements.Length; i++)
        {
            if (!targets.ContainsKey(dialogElements[i].targetName))
            {
                DialogPublisher target = GameObject.Find(dialogElements[i].targetName).GetComponent<DialogPublisher>();
                targets.Add(dialogElements[i].targetName, target);
            }
        }
        BlackTransitionCaller.Create();
        StartCoroutine(StartingDialog());
    }

    private IEnumerator StartingDialog()
    {
        yield return new WaitForSeconds(3);
        DialogUI dialogUI = Instantiate(Resources.Load<DialogUI>("Prefabs/DialogUI"));
        text_dialog = dialogUI.TextDialog;
        StartCoroutine(DialogMechanim());
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator DialogMechanim ()
    {
        while (indexDialog < dialogElements.Length - 1)
        {
            NextDialogElement();
            for (int i = 0; i < currentChars.Length; i++)
            {
                yield return new WaitForSeconds(0.03f);
                text_dialog.text += currentChars[i].ToString();
            }
            yield return new WaitForSeconds(4);
        }

        foreach (var t in targets)
        {
            t.Value.SetStatusTalk(false);
        }

        BlackTransition blackTransition = BlackTransitionCaller.Create(BlackTransitionType.Next);
        blackTransition.action += GoToLevel;

    }
        
        

    

    private void GoToLevel()
    {
        Loading.OnLoad(LevelName);
    }

    private void NextDialogElement()
    {
        indexDialog++;
        switch (LanguageManager.Language)
        {
            case Language.EN:
                currentChars = dialogElements[indexDialog].text_en_EN.ToCharArray();
                break;
            case Language.RU:
                currentChars = dialogElements[indexDialog].text_ru_RU.ToCharArray();
                break;
        }
        text_dialog.text = "";
        string targetTalk = dialogElements[indexDialog].targetName;
      foreach (var t in targets)
        {
            t.Value.SetStatusTalk(false);
        }
        targets[targetTalk].SetStatusTalk(true);
    }
}