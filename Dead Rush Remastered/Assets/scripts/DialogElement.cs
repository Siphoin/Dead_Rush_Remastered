using System;
using UnityEngine;

[Serializable]
public class DialogElement
{
    public string targetName = "";
    [TextArea]
    public string text_ru_RU = "";
    [TextArea]
    public string text_en_EN = "";

    public event Action eventEndDialog;

    public void CallEvent()
    {
        eventEndDialog?.Invoke();
    }
}
