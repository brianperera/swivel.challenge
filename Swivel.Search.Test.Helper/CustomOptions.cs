using Microsoft.Extensions.Options;
using Swivel.Search.Common;

namespace Swivel.Search.Test.Helper
{
    public class CustomOptions : IOptions<AppSettings>
    {
        public AppSettings Value => new AppSettings()
        {
            SupportEmail = "dummy1.email@fake.io",
            SysEmail = "dummy2.email@fake.io",
        };
    }
}
