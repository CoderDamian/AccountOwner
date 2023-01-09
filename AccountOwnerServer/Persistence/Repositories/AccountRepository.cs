using Entities.Models;
using Persistence.Contracts;
using Persistence.Seedwork;

namespace Persistence.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(RepositoryContext repositoryBase)
            : base(repositoryBase)
        {

        }

        public IEnumerable<Account> AccountsByOwner(string ownerId)
        {
            return FindByCondition(a => a.OwnerFK.Equals(ownerId))
                .ToList();
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return FindAll()
                .ToList();
        }
    }
}
