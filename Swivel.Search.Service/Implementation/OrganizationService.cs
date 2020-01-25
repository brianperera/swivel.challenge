using Microsoft.Extensions.Logging;
using Swivel.Search.Common;
using Swivel.Search.Model.Domain;
using Swivel.Search.Repo.Interface;
using Swivel.Search.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Swivel.Search.Service
{
    public class OrganizationService : IOrganizationService
    {
        private readonly ILogger _logger;
        private readonly IUserRepo _userRepo;
        private readonly IOrganizationRepo _organizationRepo;
        private readonly ITicketRepo _ticketRepo;

        public OrganizationService(IUserRepo userRepo, IOrganizationRepo organizationRepo, ITicketRepo ticketRepo, ILogger<IOrganizationService> logger)
        {
            _userRepo = userRepo;
            _organizationRepo = organizationRepo;
            _ticketRepo = ticketRepo;
            _logger = logger;
        }

        public List<string> GetSearchOptions()
        {
            return _organizationRepo.GetSearchOptions();
        }

        public List<GenericEntity> Search(string field, string value)
        {
            var orgQueryResult = _organizationRepo.Serach(field, value).ToList();

            if (orgQueryResult.Count() == 0)
                _logger.LogDebug($"{TextResource.NO_RESULTS_FOUND} - field: {field}, value: {value}");

            //Find dependencies and map
            foreach (var orgResult in orgQueryResult)
            {
                //Find and map dependent org info
                var organizationId = orgResult.Properties.FirstOrDefault(o => o.Key.Equals(EntityKey.ID, StringComparison.InvariantCultureIgnoreCase));

                if (organizationId.Value != null)
                {
                    var organizationResult = _organizationRepo.Serach(EntityKey.ID, organizationId.Value.ToString()).FirstOrDefault();

                    orgResult.Properties.Add(new KeyValuePair<string, object>(EntityKey.ORGANIZATION_NAME,
                        organizationResult.Properties.SingleOrDefault(o => o.Key.Equals(EntityKey.NAME, StringComparison.CurrentCultureIgnoreCase)).Value));
                }

                //Find and map dependent assigned ticket info. There can be multiple tickets assigned to a organization
                var assignedTicketId = orgResult.Properties.FirstOrDefault(o => o.Key.Equals(EntityKey.ID, StringComparison.InvariantCultureIgnoreCase));

                if (assignedTicketId.Value != null)
                {
                    var assignedTicketResults = _ticketRepo.Serach(EntityKey.ORGANIZATION_ID, assignedTicketId.Value.ToString());

                    int ticketCount = 1;

                    foreach (var ticketResult in assignedTicketResults)
                    {
                        orgResult.Properties.Add(new KeyValuePair<string, object>($"{EntityKey.ASSIGNED_TICKET}_{ticketCount}",
                        ticketResult.Properties.SingleOrDefault(o => o.Key.Equals(EntityKey.SUBJECT, StringComparison.CurrentCultureIgnoreCase)).Value));
                        ticketCount++;
                    }
                }

                //Find and map dependent assigned users info. There can be multiple users assigned to a organization
                var assignedUserId = orgResult.Properties.FirstOrDefault(o => o.Key.Equals(EntityKey.ID, StringComparison.InvariantCultureIgnoreCase));

                if (assignedUserId.Value != null)
                {
                    var assignedUserResults = _userRepo.Serach(EntityKey.ORGANIZATION_ID, assignedUserId.Value.ToString());

                    int userCount = 1;

                    foreach (var userResult in assignedUserResults)
                    {
                        orgResult.Properties.Add(new KeyValuePair<string, object>($"{EntityKey.ASSIGNED_USERS}_{userCount}",
                        userResult.Properties.SingleOrDefault(o => o.Key.Equals(EntityKey.NAME, StringComparison.CurrentCultureIgnoreCase)).Value));
                        userCount++;
                    }
                }
            }

            return orgQueryResult;
        }
    }
}
