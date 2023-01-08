using Entities.Seedwork;

namespace Entities.Models
{
    internal class Owner : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
