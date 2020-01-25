using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swivel.Search.Common;

namespace Swivel.Search.Service
{
    public class BaseService
    {        
        internal readonly ILogger<BaseService> _logger;
        internal readonly AppSettings _settings;

        public BaseService(ILogger<BaseService> logger, IOptions<AppSettings> settings)
        {
            _logger = logger;
            _settings = settings.Value;
        }
    }
}
