using System;
using UnityEngine;

public class ShopWindowPage : MonoBehaviour
{

    public event Action buyEvent;

    protected void CallBuyEvent()
    {
        if (GameCache.cacheContainer.money < 0)
        {
            GameCache.cacheContainer.money = 0;
            GameCache.WritePlayerCache();
        }
        buyEvent();


    }
}
