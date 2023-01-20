using Entities.Models;
using Persistence.Contracts;
using Persistence.Seedwork;

namespace Persistence.Repositories
{
    public class OwnerRepository : RepositoryBase<Owner>, IOwnerRepository
    {
        public OwnerRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public IEnumerable<Owner> GetAllOwners()
        {
            return FindAll()
                .OrderBy(owner => owner.Name)
                .ToList();
        }

        public Owner? GetOwnerById(string ownerId)
        {
            if (String.IsNullOrEmpty(ownerId))
                throw new ArgumentNullException(nameof(ownerId));

            Owner? owner = FindByCondition(o => o.ID.Equals(ownerId)).FirstOrDefault();

            return owner;
        }

        public Owner? GetOwnerWithDetails(string ownerId)
        {
            if (String.IsNullOrEmpty(ownerId))
                throw new ArgumentNullException(nameof(ownerId));

            Owner? owner = FindByCondition(o => o.ID.Equals(ownerId))
                .FirstOrDefault();

            return owner;
        }

        public void CreateOwner(Owner owner)
        {
            Create(owner);
        }

        public void UpdateOwner(Owner owner)
        {
            Update(owner);
        }

        public void DeleteOwner(Owner owner)
        {
            Delete(owner);
        }

        public PagedList<Owner> GetOwners(OwnerParameters ownerParameters)
        {
            return PagedList<Owner>.ToPagedList(FindAll().OrderBy(o => o.Name),
                ownerParameters.PageNumber,
                ownerParameters.PageSize);
        }
    }
}
