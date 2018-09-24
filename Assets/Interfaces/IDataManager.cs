using System.Collections.Generic;

namespace Assets.Interfaces
{
    public interface IDataManager<T>
    {
        List<T> Load();

        void Save(List<T> items);
    }
}
