/// <summary>
/// Stores a name, time, and score
/// </summary>
public class GameScore
{
    public string Name { get; set; }
    public int Score { get; set; }
    public System.TimeSpan Time { get; set; }

    /// <summary>
    /// Stores a name, time, and score
    /// </summary>
    /// <param name="name">The name of the player</param>
    /// <param name="score">The score they achieved</param>
    /// <param name="time">How long they played for</param>
    public GameScore(string name, int score, System.TimeSpan time)
    {
        Name = name;
        Score = score;
        Time = time;
    }

    /// <summary>
    /// Stores a name, time, and score
    /// </summary>
    public GameScore()
    {
        Name = "";
        Score = 0;
        Time = new System.TimeSpan();
    }

    /// <summary>
    /// Compares this GameScore to another one to see which is faster
    /// </summary>
    /// <param name="other">The other GameScore to compare to</param>
    /// <returns>True if this GameScore is faster than <c>other</c></returns>
    public bool FasterThan(GameScore other)
    {
        return other.Time.Ticks == 0 ? true : Time.CompareTo(other.Time) < 0;
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
