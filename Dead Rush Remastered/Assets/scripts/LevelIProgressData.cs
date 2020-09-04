using Newtonsoft.Json;
using System;

[Serializable]
    public  class LevelIProgressData
    {
        [JsonProperty("progress")]
        public int starsCount;
        [JsonProperty("number")]
        public int index;

    public LevelIProgressData(int Index, int starsValue)
    {
        if (starsValue > 3 || starsValue < 0)
        {
            throw new ArgumentException("Invalid value of the starsValue argument. Acceptable values are up to 0 and 3.");
        }

        if (Index < 1)
        {
            throw new ArgumentException("Invalid value for the Index argument. Acceptable values are up to 1 and > 1.");
        }

        starsCount = starsValue;
        index = Index;
    }
    }
