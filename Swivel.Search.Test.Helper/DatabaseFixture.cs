using Swivel.Search.Data;
using Swivel.Search.Test.Helper.SeedData;
using System;

namespace Swivel.Search.Test.Helper
{
    public class DatabaseFixture : IDisposable
    {
        public static bool init = false;
        private static readonly object objLock = new object();

        public DatabaseFixture()
        {
            UseInmemorySetup();
        }

        public void Dispose()
        {
            DBcontext.Dispose();
        }

        private void UseInmemorySetup()
        {
            var _dataStore = new MockDataStore();
            DBcontext = _dataStore.DataContext;
        }

        public DataContext DBcontext { get; private set; }
    }
}
