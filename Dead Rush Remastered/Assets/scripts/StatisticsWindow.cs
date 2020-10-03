using UnityEngine;
using UnityEngine.UI;

public class StatisticsWindow : Window
{
    [SerializeField] Text text_statistics;
    // Use this for initialization
    void Start()
    {
        UserStatistics statistics = StatisticsCache.Statistics;
        switch (LanguageManager.Language)
        {

            case Language.EN:
                text_statistics.text = $"Zombies killed: {CurrencyRounder.Round(statistics.zombie_kills_score)}\nDefeats: {CurrencyRounder.Round(statistics.defeats_score)}\nWins: {CurrencyRounder.Round(statistics.victory_score)}\nMoney earned: {CurrencyRounder.Round(statistics.money_earned)} $";
                break;
            case Language.RU:
                text_statistics.text = $"Зомби убито: {CurrencyRounder.Round(statistics.zombie_kills_score)}\nПоражений: {CurrencyRounder.Round(statistics.defeats_score)}\nПобед: {CurrencyRounder.Round(statistics.victory_score)}\nДенег заработано: {CurrencyRounder.Round(statistics.money_earned)} $";
                break;
        }
    }

}