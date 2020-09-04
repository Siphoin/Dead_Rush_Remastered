using UnityEngine;
using System.Collections;

public class Baricade : MonoBehaviour
{
    [SerializeField] int Health;
    [SerializeField] int armor;
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

    public void DamageBaricade (int value)
    {
        int value_convert = (value * armor / 100);
        value -= value_convert;
if (value < 0)
        {
            value = 1;
        }

        health -= value;

       
        

    }
}
