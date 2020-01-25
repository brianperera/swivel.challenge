using Swivel.Search.Service.Interface;
using Swivel.Search.Test.Helper;
using System;
using System.Linq;
using Xunit;

namespace Swivel.Search.Service.Test
{
    public class UserSearchService: TestServiceBase
    {
        public UserSearchService(DatabaseFixture fixture): base(fixture)
        {

        }

        #region Positive tests

        [Theory]
        [InlineData("_id", "1")]
        public void SearchValidUser(string field, string value)
        {
            var result = _userSearch.Search(field, value).FirstOrDefault();
            var resultUserId = result.Properties.SingleOrDefault(p => p.Key.Equals(field, StringComparison.InvariantCultureIgnoreCase)).Value;

            Assert.NotNull(resultUserId);
            Assert.Equal(value, resultUserId.ToString());
        }

        #endregion

        #region Negative tests

        [Theory]
        [InlineData("_id", "10")]
        public void SearchInvlidUser(string field, string value)
        {
            var result = _userSearch.Search(field, value).FirstOrDefault();
            Assert.Null(result);
        }

        #endregion
    }
}
