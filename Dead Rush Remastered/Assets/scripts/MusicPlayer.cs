using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] musicList;
    AudioSource audioSource;

    private float t_mixer_volume = 0;
    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (musicList == null || musicList.Length == 0)
        {
            throw new System.NullReferenceException("musicList is emtry");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            AudioClip random_clip = musicList[Random.Range(0, musicList.Length)];
            audioSource.clip = random_clip;
            audioSource.Play();
            t_mixer_volume = 0;
        }

        if (t_mixer_volume < 1)
        {
            t_mixer_volume += 0.01f;
            audioSource.volume = Mathf.Lerp(0, AudioCache.data.musicVolume, t_mixer_volume);
        }

        else
        {
            audioSource.volume = AudioCache.data.musicVolume;
        }
    }
}
