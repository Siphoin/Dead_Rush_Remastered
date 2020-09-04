using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
  public  class LevelStats
    {
    public int zombiesKilled { get; set; } = 0;

    public int moneyCost { get; set; } = 0;
    public DateTime timeLevelCompleting { get; set; } = new DateTime();

    public LevelStats (int zombies_killed, DateTime time_level_completing, int Money)
    {
        zombiesKilled = zombies_killed;
        timeLevelCompleting = time_level_completing;
        moneyCost = Money;
    }
}
