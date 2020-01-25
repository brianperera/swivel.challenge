using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swivel.Search.Common;
using Swivel.Search.Model.Domain;
using Swivel.Search.Repo.Interface;
using Swivel.Search.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Swivel.Search.Service
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IOrganizationRepo _organizationRepo;
        private readonly ITicketRepo _ticketRepo;

        public UserService(IUserRepo userRepo, IOrganizationRepo organizationRepo, ITicketRepo ticketRepo, IOptions<AppSettings> settings, ILogger<UserService> logger): base(logger, settings)
        {
            _userRepo = userRepo;
            _organizationRepo = organizationRepo;
            _ticketRepo = ticketRepo;            
        }

        public List<GenericEntity> Search(string field, string value)
        {
            var userQueryResult = _userRepo.Serach(field, value).ToList();

            if (!userQueryResult.Any())
                _logger.LogDebug($"{TextResource.NO_RESULTS_FOUND} - field: {field}, value: {value}");

            //Find dependencies and map
            foreach (var userResult in userQueryResult)
            {
                //Find and map dependent org info
                var organizationId = userResult.Properties.FirstOrDefault(o => o.Key.Equals(EntityKey.ORGANIZATION_ID, StringComparison.InvariantCultureIgnoreCase));

                if (organizationId.Value != null)
                {
                    var organizationResult = _organizationRepo.Serach(EntityKey.ID, organizationId.Value.ToString()).FirstOrDefault();

                    if (organizationResult != null)
                    {
                        userResult.Properties.Add(new KeyValuePair<string, object>(EntityKey.ORGANIZATION_NAME,
                            organizationResult.Properties.SingleOrDefault(o => o.Key.Equals(EntityKey.NAME, StringComparison.CurrentCultureIgnoreCase)).Value));
                    }
                }

                //Find and map dependent assigned ticket info. There can be multiple tickets assigned to a single user
                var assigneeId = userResult.Properties.FirstOrDefault(o => o.Key.Equals(EntityKey.ID, StringComparison.InvariantCultureIgnoreCase));

                if (assigneeId.Value != null)
                {
                    var assignedTicketResults = _ticketRepo.Serach(EntityKey.ASSIGNEE_ID, assigneeId.Value.ToString());

                    if (assignedTicketResults != null)
                    {
                        int ticketCount = 1;

                        foreach (var ticketResult in assignedTicketResults)
                        {
                            userResult.Properties.Add(new KeyValuePair<string, object>($"{EntityKey.ASSIGNED_TICKET}_{ticketCount}",
                            ticketResult.Properties.SingleOrDefault(o => o.Key.Equals(EntityKey.SUBJECT, StringComparison.CurrentCultureIgnoreCase)).Value));
                            ticketCount++;
                        }
                    }
                }

                //Find and map dependent submitted ticket info. There can be multiple tickets assigned to a single user
                var submittedTicketId = userResult.Properties.FirstOrDefault(o => o.Key.Equals(EntityKey.ID, StringComparison.InvariantCultureIgnoreCase));

                if (submittedTicketId.Value != null)
                {
                    var submittedTicketResults = _ticketRepo.Serach(EntityKey.SUBMITTER_ID, submittedTicketId.Value.ToString());

                    if (submittedTicketResults != null)
                    {
                        int ticketCount = 1;

                        foreach (var ticketResult in submittedTicketResults)
                        {
                            userResult.Properties.Add(new KeyValuePair<string, object>($"{EntityKey.SUBMITTED_TICKET}_{ticketCount}",
                            ticketResult.Properties.SingleOrDefault(o => o.Key.Equals(EntityKey.SUBJECT, StringComparison.CurrentCultureIgnoreCase)).Value));
                            ticketCount++;
                        }
                    }
                }
            }

            return userQueryResult;
        }

        public List<string> GetSearchOptions()
        {
            return _userRepo.GetSearchOptions();
        }
    }
}
