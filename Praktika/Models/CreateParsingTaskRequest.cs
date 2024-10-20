using System.ComponentModel.DataAnnotations;

namespace Praktika.Models
{
    public class CreateParsingTaskRequest
    {



        public int Id { get; set; }
        
        public string Name { get; set; }

        public string? ExportMethod { get; set; }

    }
}
