using Entities.Models;
using Microsoft.EntityFrameworkCore;
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
                .Include(o => o.Accounts)
                .FirstOrDefault();

            return owner;
        }
    }
}
