using System;
[Serializable]
 public   class SurvivalRecord
    {
    [Newtonsoft.Json.JsonProperty("time")]
   public DateTime timeRecord;
 public   int kills;
    }
