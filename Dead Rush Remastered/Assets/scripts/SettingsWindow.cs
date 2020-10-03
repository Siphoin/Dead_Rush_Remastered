using UnityEngine;
using UnityEngine.UI;

public class SettingsWindow : Window
{
    [SerializeField] Slider slider_fx;
    [SerializeField] Slider slider_music;
    // Use this for initialization
    void Start()
    {
        slider_fx.value = AudioCache.data.fxVolume;
        slider_music.value = AudioCache.data.musicVolume;
        SetChildEvent(SaveData);
    }

    private void SaveData()
    {

            AudioCache.SaveData();
        
    }

    // Update is called once per frame
    void Update()
    {
        AudioCache.data.fxVolume = slider_fx.value;
        AudioCache.data.musicVolume = slider_music.value;
    }

    
}
