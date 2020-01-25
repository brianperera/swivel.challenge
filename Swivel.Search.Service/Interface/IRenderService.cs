using Swivel.Search.Model.Domain;
using System;
using System.Collections.Generic;

namespace Swivel.Search.Service.Interface
{
    public interface IRenderService
    {
        void Render(List<GenericEntity> result, string type);

        void Render(List<string> items, string type);

        CommandOutput ResolveCommand();
    }
}
