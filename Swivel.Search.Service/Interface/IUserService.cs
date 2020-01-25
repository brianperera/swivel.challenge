using Swivel.Search.Model.Domain;
using System;
using System.Collections.Generic;

namespace Swivel.Search.Service.Interface
{
    public interface IUserService
    {
        List<GenericEntity> Search(string field, string value);

        List<string> GetSearchOptions();
    }
}
