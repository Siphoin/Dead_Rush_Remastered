using System;
using UnityEngine;

public class DialogMechanimScript : MonoBehaviour
{
    protected DialogAction[] actionsContainer;
    // Use this for initialization
    void Start()
    {
    }



    // Update is called once per frame
    void Update()
    {

    }

    protected void PublishEvent(Action action, int index)
    {
        DialogComponent.SetDialogKey(action, index);
    }
}
