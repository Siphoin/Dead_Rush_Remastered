using UnityEngine;
public class VampireZombie : MonoBehaviour
    {
        public ZombieBase Zombiebase { get; private set; }

        // Use this for initialization
        void Start()
        {
            Zombiebase = GetComponent<ZombieBase>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Zombie")
            {
                if (Zombiebase.Health < Zombiebase.StartedHealth)
                {
                    if (transform.position.x >= collision.transform.position.x)
                    {
                      
ZombieBase zombie = collision.GetComponent<ZombieBase>();
                        if (!zombie.IsGhost)
                        {
                    Zombiebase.HillZombie(zombie.StartedHealth / 2);
                    zombie.Damage(zombie.StartedHealth);
                        }

                    }
                    

                }
            }
        }
    }