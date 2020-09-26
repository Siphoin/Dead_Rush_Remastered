using UnityEngine;

public class EndGame : MonoBehaviour
    {


        public void SelectVariant (bool OnDelete)
        {
            if (OnDelete)
            {
                GameCache.Player_cacheContainer = new GameCacheContainer();
                GameCache.DeleteSave();
            }

            Menu.OnPageLevels = false;
            Loading.OnLoad("Menu", false);
        }
    }