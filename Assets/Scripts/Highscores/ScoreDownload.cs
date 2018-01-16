using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class ScoreDownload
{
    /// <summary>
    /// An array of scores for the highest recorded scores
    /// </summary>
    [JsonProperty("scoreScores")]
    public GameScore[] ScoreScores { get; set; }

    /// <summary>
    /// An array of scores for the quickest recorded times
    /// </summary>
    [JsonProperty("timeScores")]
    public GameScore[] TimeScores { get; set; }
}
