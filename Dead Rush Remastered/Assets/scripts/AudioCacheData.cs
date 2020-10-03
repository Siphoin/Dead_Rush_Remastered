using System;

[Serializable]
public class AudioCacheData
{
    [Newtonsoft.Json.JsonProperty("fx_value")]

    public float fxVolume = 1f;
    [Newtonsoft.Json.JsonProperty("music_value")]
    public float musicVolume = 1f;
}
