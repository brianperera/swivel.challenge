using Swivel.Search.Test.Helper;
using System;
using System.Linq;
using Xunit;

namespace Swivel.Search.Service.Test
{
    public class TicketSearchService : TestServiceBase
    {
        public TicketSearchService(DatabaseFixture fixture): base(fixture)
        {

        }

        #region Positive tests

        [Theory]
        [InlineData("_id", "daf8d797-3d09-4c93-9f3b-a642b63ded99")]
        public void SearchValidTicket(string field, string value)
        {
            var result = _ticketSearch.Search(field, value).FirstOrDefault();
            var resultOrgId = result.Properties.SingleOrDefault(p => p.Key.Equals(field, StringComparison.InvariantCultureIgnoreCase)).Value;

            Assert.NotNull(resultOrgId);
            Assert.Equal(value, resultOrgId.ToString());
        }

        #endregion

        #region Negative tests

        [Theory]
        [InlineData("_id", "ID12345-WW3")]
        [InlineData("expiry_date", "1/25/2020")]
        public void SearchInvlidTicketValue(string field, string value)
        {
            var result = _ticketSearch.Search(field, value).FirstOrDefault();
            Assert.Null(result);
        }

        #endregion
    }
}
