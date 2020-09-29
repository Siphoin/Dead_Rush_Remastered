using UnityEngine;
using System.Collections;

public class AudioObject : MonoBehaviour
{
    [SerializeField] TypeAudio type;
    private AudioSource audioSource;
    [Range(0f, 1f)]
    [SerializeField]
    private float range_pitch = 0.4f;

    [SerializeField] bool PlayOnAwake = true;

    [SerializeField] bool random_pich = true;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (!PlayOnAwake)
        {
            return;
        }
        if (!random_pich)
        {
            return;
        }
        if (type == TypeAudio.FX)
        {
            audioSource.pitch = Random.Range(range_pitch, 2f);
        }

        audioSource.Play();
    }

    private void Update()
    {
        if (type == TypeAudio.FX)
        {
            audioSource.volume = AudioCache.data.fxVolume;

        }
    }

}