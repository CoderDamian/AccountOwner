using Persistence.Repositories;

namespace Persistence.Contracts
{
    internal interface IRepositoryWrapper
    {
        public AccountRepository AccountRepository { get; }
        public OwnerRepository OwnerRepository { get; }
        void Save();
    }
}
