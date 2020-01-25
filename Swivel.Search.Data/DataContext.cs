using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Swivel.Search.Data
{
    public class DataContext
    {
        public JArray Users { get; set; }

        public JArray Tickets { get; set; }

        public JArray Organizations { get; set; }

        public void Dispose()
        {
            //This would ideally handle the disposing of data
        }
    }
}
