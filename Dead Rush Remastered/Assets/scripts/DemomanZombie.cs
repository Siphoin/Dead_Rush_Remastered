using UnityEngine;
public class DemomanZombie : MonoBehaviour, INotReward
{
    public ZombieBase Zombiebase { get; private set; }

    [SerializeField]

    private int damage;

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
        switch (collision.tag)
        {
            case "Baricades":
                Baricade baricade = collision.GetComponent<Baricade>();
                baricade.Damage(damage);
                CallNullReward();
                Zombiebase.Damage(Zombiebase.StartedHealth);
                break;

            case "Player":
                Character player = collision.GetComponent<Character>();
                player.Damage(damage);
                CallNullReward();
                Zombiebase.Damage(Zombiebase.StartedHealth);
                break;

            case "Partner":
                Partner partner = collision.GetComponent<Partner>();
                partner.Damage(damage);
                Zombiebase.Damage(Zombiebase.StartedHealth);
                break;
        }
    }

    public void CallNullReward()
    {
        Zombiebase.OnNullReward();
    }
}