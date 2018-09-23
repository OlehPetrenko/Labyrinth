using System.Collections.Generic;
using Assets.Classes;

namespace Assets.Interfaces
{
    public interface IDataManager<T>
    {
        List<T> Load();

        void Save(List<T> items);
    }
}
