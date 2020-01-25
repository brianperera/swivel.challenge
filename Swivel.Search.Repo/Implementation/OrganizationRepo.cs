using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Swivel.Search.Data;
using Swivel.Search.Model.Domain;
using Swivel.Search.Repo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Swivel.Search.Repo
{
    public class OrganizationRepo : BaseRepo, IOrganizationRepo
    {
        public OrganizationRepo(IDataStore dataStore, ILogger<OrganizationRepo> logger) : base(dataStore, logger)
        {
            
        }

        public List<string> GetSearchOptions()
        {
            var queryResult = _dataContext.Organizations.OfType<JObject>()
                .Select(p => p).FirstOrDefault();

            var tokens = queryResult.Children().Select(s => ((JProperty)s).Name);

            return tokens.ToList();
        }

        public IEnumerable<GenericEntity> Serach(string field, string value)
        {
            var queryResult = _dataContext.Organizations.OfType<JObject>()
                .Where(p => p.Property(field) != null && p.Property(field).Name.Equals(field, StringComparison.InvariantCultureIgnoreCase)
                && p.Property(field).Value.ToString().Equals(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(p => new GenericEntity()
                {
                    Properties = MapProperties(p)
                });

            return queryResult;
        }
    }
}
