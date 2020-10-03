using System.Collections;
using UnityEngine;

namespace Assets.scripts
{
    public class HordeCaller : MonoBehaviour
    {
        [SerializeField] private float time_spawn = 20;
        [SerializeField] private int count_zombies = 7;

        [SerializeField] ZombieBase prefab;
        // Use this for initialization
        void Start()
        {


            StartCoroutine(NewHorde());
        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator NewHorde()
        {
            while (true)
            {
                yield return new WaitForSeconds(time_spawn);
                LevelManager.manager.CallCustomHorde(count_zombies, prefab);
            }
        }
    }
}