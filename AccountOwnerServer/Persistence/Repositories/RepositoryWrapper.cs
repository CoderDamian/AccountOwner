using Persistence.Contracts;

namespace Persistence.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly RepositoryContext _repositoryContext;
        private IAccountRepository _accountRepository;
        private IOwnerRepository _ownerRepository;

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            this._repositoryContext = repositoryContext;
        }

        public IAccountRepository AccountRepository
        {
            get {
                if (_accountRepository == null)
                    _accountRepository = new AccountRepository(_repositoryContext);

                return _accountRepository;
            }
        }

        public IOwnerRepository OwnerRepository
        {
            get {
                if (_ownerRepository == null)
                    _ownerRepository = new OwnerRepository(_repositoryContext);

                return _ownerRepository;
            }
        }

        public void Save()
        {
            _repositoryContext.SaveChanges();
        }
    }
}
