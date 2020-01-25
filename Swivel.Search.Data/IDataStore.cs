using System;

namespace Swivel.Search.Data
{
    public interface IDataStore
    {
        DataContext DataContext { get; set; }
    }
}
