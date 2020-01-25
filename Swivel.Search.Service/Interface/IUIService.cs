using Swivel.Search.Model.Domain;
using System.Collections.Generic;

namespace Swivel.Search.Service.Interface
{
    public interface IUIService
    {
        void Render(List<GenericEntity> result, string type);

        void Render(List<string> items, string type);

        CommandOutput ResolveCommand();
    }
}
