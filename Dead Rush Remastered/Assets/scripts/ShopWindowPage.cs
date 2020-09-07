using UnityEngine;
using System;

public class ShopWindowPage : MonoBehaviour
{

    public event Action buyEvent;

    protected void CallBuyEvent ()
    {
        buyEvent();
    }
}
