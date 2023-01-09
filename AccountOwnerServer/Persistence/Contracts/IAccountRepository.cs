using Entities.Models;

namespace Persistence.Contracts
{
    public interface IAccountRepository
    {
        IEnumerable<Account> AccountsByOwner(string ownerId);
        IEnumerable<Account> GetAllAccounts();
    }
}
