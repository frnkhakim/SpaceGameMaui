using System.Text.Json;
using Microsoft.Maui.Storage;

namespace SpaceGameMaui
{
    public class HighScoreService
    {
        private const string HighScoreKey = "spacegame_win_scores_v1";

        public int RecordWinAndGetRank(int score, double timeSeconds)
        {
            var entries = LoadEntries();
            var newEntry = new HighScoreEntry
            {
                Score = score,
                TimeSeconds = timeSeconds,
                PlayedAtUtc = DateTime.UtcNow
            };

            entries.Add(newEntry);
            SaveEntries(entries);

            var ordered = SortWins(entries);
            var rank = ordered.FindIndex(e =>
                e.PlayedAtUtc == newEntry.PlayedAtUtc &&
                e.Score == newEntry.Score &&
                Math.Abs(e.TimeSeconds - newEntry.TimeSeconds) < 0.0001
            );

            return rank >= 0 ? rank + 1 : ordered.Count;
        }

        public List<HighScoreEntry> GetTop10Wins()
        {
            return SortWins(LoadEntries())
                .Take(10)
                .ToList();
        }

        private static List<HighScoreEntry> SortWins(List<HighScoreEntry> entries)
        {
            return entries
                .OrderBy(e => e.TimeSeconds)
                .ThenByDescending(e => e.Score)
                .ThenBy(e => e.PlayedAtUtc)
                .ToList();
        }

        private static List<HighScoreEntry> LoadEntries()
        {
            var json = Preferences.Default.Get(HighScoreKey, string.Empty);
            if (string.IsNullOrWhiteSpace(json))
                return new List<HighScoreEntry>();

            try
            {
                return JsonSerializer.Deserialize<List<HighScoreEntry>>(json) ?? new List<HighScoreEntry>();
            }
            catch
            {
                return new List<HighScoreEntry>();
            }
        }

        private static void SaveEntries(List<HighScoreEntry> entries)
        {
            var json = JsonSerializer.Serialize(entries);
            Preferences.Default.Set(HighScoreKey, json);
        }
    }
}
