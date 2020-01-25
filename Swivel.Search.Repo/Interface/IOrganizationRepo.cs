using Swivel.Search.Model.Domain;
using System;
using System.Collections.Generic;

namespace Swivel.Search.Repo.Interface
{
    public interface IOrganizationRepo
    {
        IEnumerable<GenericEntity> Serach(string field, string value);
        List<string> GetSearchOptions();
    }
}
