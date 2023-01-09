namespace Entities.DTOs
{
    public class OwnerWithAccountsDTO
    {
        public string Id { get; set; } = string.Empty;
        public string? Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }

        public IEnumerable<AccountDTO> AccountDTOs { get; set; } = null!;
    }
}
