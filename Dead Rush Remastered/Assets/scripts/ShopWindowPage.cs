using System;
using UnityEngine;

public class ShopWindowPage : MonoBehaviour
{

    public event Action buyEvent;

    protected void CallBuyEvent()
    {
        if (GameCache.player_cacheContainer.money < 0)
        {
            GameCache.player_cacheContainer.money = 0;
            GameCache.SaveData();
        }
        buyEvent();


    }
}
