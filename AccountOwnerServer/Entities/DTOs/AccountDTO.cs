namespace Entities.DTOs
{
    public class AccountDTO
    {
        public string Id { get; set; } = String.Empty;
        public DateTime DateCreated { get; set; }
        public string AccountType { get; set; } = string.Empty;
        public string OwnerFK { get; set; } = string.Empty;
    }
}
