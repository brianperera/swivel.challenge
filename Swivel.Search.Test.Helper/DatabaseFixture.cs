using Swivel.Search.Data;
using Swivel.Search.Test.Helper.SeedData;

namespace Swivel.Search.Test.Helper
{
    public class DatabaseFixture
    {
        public DatabaseFixture()
        {
            UseInmemorySetup();
        }

        private void UseInmemorySetup()
        {
            var _dataStore = new MockDataStore();
            DBcontext = _dataStore.DataContext;
        }

        public DataContext DBcontext { get; private set; }
    }
}
