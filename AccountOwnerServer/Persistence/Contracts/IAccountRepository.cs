using Entities.Models;

namespace Persistence.Contracts
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAccountsByOwner(string ownerId);
        IEnumerable<Account> GetAllAccounts();
    }
}
