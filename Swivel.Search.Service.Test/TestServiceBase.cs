using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swivel.Search.Common;
using Swivel.Search.Repo;
using Swivel.Search.Repo.Interface;
using Swivel.Search.Service.Interface;
using Swivel.Search.Test.Helper;
using Swivel.Search.Test.Helper.SeedData;
using Xunit;

namespace Swivel.Search.Service.Test
{
    public class TestServiceBase: IClassFixture<DatabaseFixture>
    {
        public readonly DatabaseFixture _fixture;
        internal IUserService _userSearch = null;
        internal ITicketService _ticketSearch = null;
        internal IOrganizationService _organizationSearch = null;
        internal IUserRepo _userRepo = null;
        internal ITicketRepo _ticketRepo = null;
        internal IOrganizationRepo _organizationRepo = null;
        protected IOptions<AppSettings> settings = new CustomOptions();

        public TestServiceBase(DatabaseFixture fixture)
        {
            _fixture = fixture;
            InitCommonRepo();
            InitCommonService();
        }

        private void InitCommonRepo()
        {
            _userRepo = new UserRepo(new MockDataStore(), GetLogger<UserRepo>());
            _organizationRepo = new OrganizationRepo(new MockDataStore(), GetLogger<OrganizationRepo>());
            _ticketRepo = new TicketRepo(new MockDataStore(), GetLogger<TicketRepo>());
        }

        private void InitCommonService()
        {
            _userSearch = new UserService(_userRepo, _organizationRepo, _ticketRepo, settings, GetLogger<UserService>());
            _ticketSearch = new TicketService(_userRepo, _organizationRepo, _ticketRepo, settings, GetLogger<TicketService>());
            _organizationSearch = new OrganizationService(_userRepo, _organizationRepo, _ticketRepo, settings, GetLogger<OrganizationService>());
        }

        public ILogger<T> GetLogger<T>() where T : class
        {
            return new LoggerFactory().CreateLogger<T>();
        }
    }
}
