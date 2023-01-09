namespace Entities.DTOs
{
    public class OwnerDto
    {
        public string Id { get; set; } = string.Empty;
        public string? Name { get; set; } 
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
    }
}
