using System.ComponentModel.DataAnnotations;

namespace Praktika.Models.Entitys
{
    public class TasksEntity
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? NumberOfLines { get; set; }

        public List<string>? Data { get; set; }


    }
}