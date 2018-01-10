using Newtonsoft.Json;

/// <summary>
/// Stores a name, time, and score
/// </summary>
public class GameScore
{
    [JsonProperty("Name")]
    public string Name { get; set; }
    [JsonProperty("Score")]
    public int Score { get; set; }
    [JsonProperty("Duration")]
    public int Duration { get; set; }

    /// <summary>
    /// Stores a name, time, and score
    /// </summary>
    /// <param name="name">The name of the player</param>
    /// <param name="score">The score they achieved</param>
    /// <param name="time">How long they played for</param>
    public GameScore(string name, int score, int time)
    {
        Name = name;
        Score = score;
        Duration = time;
    }

    /// <summary>
    /// Stores a name, time, and score
    /// </summary>
    public GameScore()
    {
        Name = "No name";
        Score = 0;
        Duration = 0;
    }

    /// <summary>
    /// Compares this GameScore to another one to see which is faster
    /// </summary>
    /// <param name="other">The other GameScore to compare to</param>
    /// <returns>True if this GameScore is faster than <c>other</c></returns>
    public bool FasterThan(GameScore other)
    {
        return other.Duration == 0 ? true : Duration < other.Duration;
    }
    
    /// <summary>
    /// Compares this GameScore to another one to see which scored more
    /// </summary>
    /// <param name="other">The other GameScore to compare to</param>
    /// <returns>True if this GameScore scored more than <c>other</c></returns>
    public bool ScoredMoreThan(GameScore other)
    {
        return Score > other.Score;
    }
}
