using System;

namespace Assets.Classes
{
    /// <summary>
    /// The lightweight object to keep score data.
    /// </summary>
    [Serializable]
    public sealed class ScoreItemDto
    {
        public string Name;
        public string Score;
        public string Duration;
        public string Date;
        public string Result;
    }
}
