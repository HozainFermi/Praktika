using System.Security.Policy;

namespace Praktika.Models
{
    public class ParsRequestModel
    {
        
        public Url SiteUrl { get; set; }
        public string? cssSelector { get; set; }
        public string? XPATH { get; set; }



    }
}
