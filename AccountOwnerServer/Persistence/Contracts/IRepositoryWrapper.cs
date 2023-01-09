using Persistence.Repositories;

namespace Persistence.Contracts
{
    public interface IRepositoryWrapper
    {
        public IAccountRepository AccountRepository { get; }
        public IOwnerRepository OwnerRepository { get; }
        void Save();
    }
}
