using UnityEngine;
using System.Collections;

public abstract class Ball : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected int damage;
    protected Baricade baricade;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void FindBaricade()
    {
        try
        {
            baricade = GameObject.FindGameObjectWithTag("Baricades").GetComponent<Baricade>();
        }

        catch
        {

        }

    }
}
