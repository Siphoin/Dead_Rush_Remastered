using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public class TransliteString
{
    [TextArea]
    [JsonProperty("en")]
    public string string_en_EN = "";
    [TextArea]
    [JsonProperty("ru")]
    public string string_ru_RU = "";
}
