using Entities.Seedwork;

namespace Entities.Models
{
    internal class Account : EntityBase
    {
        public DateTime DateCreated { get; set; }
        public string AccountType { get; set; } = string.Empty;
        public string OwnerFK { get; set; } = string.Empty;
    }
}
