using UnityEngine;

public class Canister : MonoBehaviour, IDieAudio
{



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            // create emtry object Canister
            CanisterEmtry canisterEmtry = Instantiate(Resources.Load<CanisterEmtry>("Prefabs/canister_emtry"));
            canisterEmtry.transform.position = transform.position;
            CanisterEffect blood = Instantiate(Resources.Load<CanisterEffect>("Prefabs/canister_effect"));
            blood.transform.position = transform.position;
            PlayAudioDie();
            Destroy(gameObject);
        }
    }

    public void PlayAudioDie()
    {
        Instantiate(Resources.Load<AudioSource>("fx_prefabs/canister_die"));
    }
}