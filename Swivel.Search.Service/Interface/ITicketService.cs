using Swivel.Search.Model.Domain;
using System.Collections.Generic;

namespace Swivel.Search.Service.Interface
{
    public interface ITicketService
    {
        List<GenericEntity> Search(string field, string value);

        List<string> GetSearchOptions();
    }
}
