namespace Praktika.Models.Entitys
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        
        public string? Email { get; set; }
        public string? Password { get; set; }

    }
}
