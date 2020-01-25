using Swivel.Search.Test.Helper;
using System;
using System.Linq;
using Xunit;

namespace Swivel.Search.Service.Test
{
    public class OrganizationSearchService : TestServiceBase
    {
        public OrganizationSearchService(DatabaseFixture fixture): base(fixture)
        {

        }

        #region Positive tests

        [Theory]
        [InlineData("_id", "119")]
        public void SearchValidOrganization(string field, string value)
        {
            var result = _organizationSearch.Search(field, value).FirstOrDefault();
            var resultOrgId = result.Properties.SingleOrDefault(p => p.Key.Equals(field, StringComparison.InvariantCultureIgnoreCase)).Value;

            Assert.NotNull(resultOrgId);
            Assert.Equal(value, resultOrgId.ToString());
        }

        #endregion

        #region Negative tests

        [Theory]
        [InlineData("_id", "101010")]
        [InlineData("employee_count", "10000")]
        public void SearchInvlidUserValue(string field, string value)
        {
            var result = _organizationSearch.Search(field, value).FirstOrDefault();
            Assert.Null(result);
        }

        #endregion
    }
}
