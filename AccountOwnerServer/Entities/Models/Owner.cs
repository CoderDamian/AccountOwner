using Entities.Seedwork;

namespace Entities.Models
{
    public class Owner : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;
        //public ICollection<Account> Accounts { get; set; }
    }
}
