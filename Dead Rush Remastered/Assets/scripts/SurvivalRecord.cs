using System;
[Serializable]
public class SurvivalRecord
{
    [Newtonsoft.Json.JsonProperty("time")]
    public DateTime timeRecord = new DateTime();
    public int kills;
}
