using Entities.Models;

namespace Persistence.Contracts
{
    public interface IOwnerRepository 
    {
        IEnumerable<Owner> GetAllOwners();
        Owner? GetOwnerById(string ownerId);
        Owner? GetOwnerWithDetails(string ownerId);
    }
}
