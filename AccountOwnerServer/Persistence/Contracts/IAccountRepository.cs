using Entities.Models;

namespace Persistence.Contracts
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAllAccounts();
    }
}
