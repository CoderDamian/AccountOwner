using Entities.Models;

namespace Persistence.Contracts
{
    public interface IOwnerRepository 
    {
        void CreateOwner(Owner owner);
        void DeleteOwner(Owner owner);
        IEnumerable<Owner> GetAllOwners();
        PagedList<Owner> GetOwners(OwnerParameters ownerParameters);
        Owner? GetOwnerById(string ownerId);
        Owner? GetOwnerWithDetails(string ownerId);
        void UpdateOwner(Owner owner);
    }
}
