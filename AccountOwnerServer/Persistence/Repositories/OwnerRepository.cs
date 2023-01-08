using Entities.Models;
using Persistence.Contracts;
using Persistence.Seedwork;

namespace Persistence.Repositories
{
    internal class OwnerRepository : RepositoryBase<Owner>, IOwnerRepository
    {
        public OwnerRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }
    }
}
