using Swivel.Search.Model.Domain;
using System.Collections.Generic;

namespace Swivel.Search.Repo.Interface
{
    public interface ITicketRepo
    {
        IEnumerable<GenericEntity> Serach(string field, string value);
        List<string> GetSearchOptions();
    }
}
