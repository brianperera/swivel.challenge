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
    public class TicketService : ITicketService
    {
        private readonly ILogger _logger;
        private readonly IUserRepo _userRepo;
        private readonly IOrganizationRepo _organizationRepo;
        private readonly ITicketRepo _ticketRepo;

        public TicketService(IUserRepo userRepo, IOrganizationRepo organizationRepo, ITicketRepo ticketRepo, ILogger<ITicketService> logger)
        {
            _userRepo = userRepo;
            _organizationRepo = organizationRepo;
            _ticketRepo = ticketRepo;
            _logger = logger;
        }

        public List<string> GetSearchOptions()
        {
            return _ticketRepo.GetSearchOptions();
        }

        public List<GenericEntity> Search(string field, string value)
        {
            var ticketQueryResult = _ticketRepo.Serach(field, value).ToList();

            if (ticketQueryResult.Count() == 0)
                _logger.LogDebug($"{TextResource.NO_RESULTS_FOUND} - field: {field}, value: {value}");

            //Find dependencies and map
            foreach (var ticketResult in ticketQueryResult)
            {
                //Find and map dependent org info
                var organizationId = ticketResult.Properties.FirstOrDefault(o => o.Key.Equals(EntityKey.ORGANIZATION_ID, StringComparison.InvariantCultureIgnoreCase));

                if (organizationId.Value != null)
                {
                    var organizationResult = _organizationRepo.Serach(EntityKey.ID, organizationId.Value.ToString()).FirstOrDefault();

                    if (organizationResult != null)
                    {
                        ticketResult.Properties.Add(new KeyValuePair<string, object>(EntityKey.ORGANIZATION_NAME,
                            organizationResult.Properties.SingleOrDefault(o => o.Key.Equals(EntityKey.NAME, StringComparison.CurrentCultureIgnoreCase)).Value));
                    }
                }

                //Find and map dependent assigned users info. There can be multiple users assigned to a organization
                var assigneeId = ticketResult.Properties.FirstOrDefault(o => o.Key.Equals(EntityKey.ASSIGNEE_ID, StringComparison.InvariantCultureIgnoreCase));

                if (assigneeId.Value != null)
                {
                    var assignedUserResults = _userRepo.Serach(EntityKey.ID, assigneeId.Value.ToString());

                    if (assignedUserResults != null)
                    {
                        int userCount = 1;

                        foreach (var userResult in assignedUserResults)
                        {
                            ticketResult.Properties.Add(new KeyValuePair<string, object>($"{EntityKey.ASSIGNED_USERS}_{userCount}",
                            userResult.Properties.SingleOrDefault(o => o.Key.Equals(EntityKey.NAME, StringComparison.CurrentCultureIgnoreCase)).Value));
                            userCount++;
                        }
                    }
                }

                //Find and map dependent submitted users info. There can be multiple users assigned to a organization
                var submitterId = ticketResult.Properties.FirstOrDefault(o => o.Key.Equals(EntityKey.SUBMITTER_ID, StringComparison.InvariantCultureIgnoreCase));

                if (submitterId.Value != null)
                {
                    var submittedUserResults = _userRepo.Serach(EntityKey.ID, submitterId.Value.ToString());

                    if (submittedUserResults != null)
                    {
                        int userCount = 1;

                        foreach (var userResult in submittedUserResults)
                        {
                            ticketResult.Properties.Add(new KeyValuePair<string, object>($"{EntityKey.SUBMITTED_USERS}_{userCount}",
                            userResult.Properties.SingleOrDefault(o => o.Key.Equals(EntityKey.NAME, StringComparison.CurrentCultureIgnoreCase)).Value));
                            userCount++;
                        }
                    }
                }
            }

            return ticketQueryResult;
        }
    }
}
