using UnityEngine;

public class Baricade : MonoBehaviour, IHPObject
{
    [SerializeField] int Health;
    [SerializeField] int armor;
    [SerializeField] int Sale;
    public int health { get => Health; set => Health = value; }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Damage(int value)
    {
        int value_convert = (value * armor / 100);
        value -= value_convert;
        if (value < 0)
        {
            value = 1;
        }

        health -= value;




    }

    public BaricadeData GetData()
    {
        return new BaricadeData() { armor = armor, health = Health, name_prefab = gameObject.name, sale = Sale };
    }
}
