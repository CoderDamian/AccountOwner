namespace Entities.Models
{
    public class OwnerParameters : QueryStringParameters
    {
        public uint MinYearOfBirth { get; set; } = 0;
        public uint MaxYearOfBirth { get; set; } = (uint)DateTime.Now.Year;
        public bool ValidYearRange => MaxYearOfBirth > MinYearOfBirth;
        public string Name { get; set; } = string.Empty;

        public OwnerParameters()
        {
            OrderBy = "name";
        }
    }
}