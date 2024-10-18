using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace Praktika.Models
{
    public class ParsRequestModel
    {
        
        public Uri SiteUrl { get; set; }
        public List<string> Selectors { get; set; }
        public string SelectorsType { get; set; }

       // public List<string>? Data { get; set; }
    }
}
