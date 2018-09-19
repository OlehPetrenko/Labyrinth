using System.Collections.Generic;
using Assets.Classes;

namespace Assets.Interfaces
{
    public interface IScoresDataManager
    {
        List<ScoreItemDto> Load();

        void Save(List<ScoreItemDto> items);
    }
}
