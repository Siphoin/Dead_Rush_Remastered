using System;
using UnityEngine.Events;

[Serializable]
public class DialogAction
{
    public int index { get; set; } = 0;
    public event UnityAction action;

    public DialogAction(int Index, UnityAction Action)
    {
        action += Action;
        index = Index;
    }
}
