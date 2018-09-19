using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Classes
{
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
