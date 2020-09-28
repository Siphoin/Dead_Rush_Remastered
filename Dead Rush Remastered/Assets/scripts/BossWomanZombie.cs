using UnityEngine;
using System.Collections;

namespace Assets.scripts
{
    public class BossWomanZombie : MonoBehaviour
    {
        private const float TIME_SPAWN = 20;
        private const int COUNT_ZOMBIES = 7;
     
        [SerializeField] ZombieBase prefab;
        // Use this for initialization
        void Start()
        {
            if (prefab == null)
            {
                throw new PrefabNullException("prefab is null");
            }

            StartCoroutine(NewHorde());
        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator NewHorde ()
        {
            while (true)
            {
                yield return new WaitForSeconds(TIME_SPAWN);
                LevelManager.manager.CallCustomHorde(COUNT_ZOMBIES, prefab);
            }
        }
    }
}