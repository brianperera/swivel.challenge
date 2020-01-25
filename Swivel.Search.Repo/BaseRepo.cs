using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Swivel.Search.Data;
using System.Collections.Generic;
using System.Linq;

namespace Swivel.Search.Repo
{
    public class BaseRepo
    {
        internal readonly ILogger<BaseRepo> _logger;
        internal readonly DataContext _dataContext;

        public BaseRepo(IDataStore dataStore, ILogger<BaseRepo> logger)
        {
            _dataContext = dataStore.DataContext;
            _logger = logger;
        }

        #region Helpers

        internal List<KeyValuePair<string, object>> MapProperties(JObject obj)
        {
            if (obj == null)
                return new List<KeyValuePair<string, object>>();

            return obj.Descendants().OfType<JProperty>().Select(d => MapProperty(d)).ToList();
        }

        private KeyValuePair<string, object> MapProperty(JToken prop)
        {
            return new KeyValuePair<string, object>(((JProperty)prop).Name, ((JProperty)prop).Value);
        }

        #endregion
    }
}
