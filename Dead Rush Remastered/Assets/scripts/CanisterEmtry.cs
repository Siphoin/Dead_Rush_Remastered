using UnityEngine;
using System.Collections;
[RequireComponent(typeof(TimerDestroy))]
    public class CanisterEmtry : MonoBehaviour
    {
        private const int DAMAGE = 15;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Zombie")
            {
                ZombieBase zombie = collision.GetComponent<ZombieBase>();
                zombie.Damage(DAMAGE);
            }
        }
    }