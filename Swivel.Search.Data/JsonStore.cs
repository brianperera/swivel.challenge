using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Reflection;

namespace Swivel.Search.Data
{
    public class JsonStore : IDataStore
    {
        public DataContext DataContext { get; set; }

        public JsonStore()
        {
            DataContext = InitDataContext();
        }

        public DataContext InitDataContext()
        {
            string userDataString = File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\users.json"));
            string orgDataString = File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\organizations.json"));
            string ticketDataString = File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\tickets.json"));

            return new DataContext()
            {
                Users = JArray.Parse(userDataString),
                Organizations = JArray.Parse(orgDataString),
                Tickets = JArray.Parse(ticketDataString)
            };
        }
    }
}
