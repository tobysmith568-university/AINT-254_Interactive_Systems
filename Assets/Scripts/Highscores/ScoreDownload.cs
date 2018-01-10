using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class ScoreDownload
{
    [JsonProperty("scoreScores")]
    public GameScore[] ScoreScores { get; set; }
    [JsonProperty("timeScores")]
    public GameScore[] TimeScores { get; set; }
}
