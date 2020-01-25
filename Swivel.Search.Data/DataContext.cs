using Newtonsoft.Json.Linq;

namespace Swivel.Search.Data
{
    public class DataContext
    {
        public JArray Users { get; set; }

        public JArray Tickets { get; set; }

        public JArray Organizations { get; set; }
    }
}
