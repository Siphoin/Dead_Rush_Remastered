using UnityEngine;
using System.Collections;

    public class Canister : MonoBehaviour
    {

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
        if (collision.tag == "Bullet")
        {
            // create emtry object Canister
            CanisterEmtry canisterEmtry = Instantiate(Resources.Load<CanisterEmtry>("Prefabs/canister_emtry"));
            canisterEmtry.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}