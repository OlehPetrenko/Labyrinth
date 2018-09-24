using System.Collections.Generic;

namespace Assets.Interfaces
{
    /// <summary>
    /// Represents a object to work with data.
    /// </summary>
    /// <typeparam name="T">The type of data to manage.</typeparam>
    public interface IDataManager<T>
    {
        List<T> Load();

        void Save(List<T> items);
    }
}
