using Entities.Models;
using Persistence.Contracts;
using Persistence.Seedwork;
using System.Drawing;
using System.Reflection;
using System.Text;

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
            IQueryable<Owner> owners = FindAll()
                .Where(o => o.DateOfBirth.Year >= ownerParameters.MinYearOfBirth && o.DateOfBirth.Year <= ownerParameters.MaxYearOfBirth)
                .OrderBy(o => o.Name);

            SearchByName(ref owners, ownerParameters.Name);

            return PagedList<Owner>.ToPagedList(owners,
                ownerParameters.PageNumber,
                ownerParameters.PageSize);
        }

        private void SearchByName(ref IQueryable<Owner> owners, string name)
        {
            if (!String.IsNullOrWhiteSpace(name))
            {
                owners.Where(o => o.Name.ToLower().Contains(name.Trim().ToLower()));
            }
        }

        // We need System.Linq.Dynamic.Core nuGet package
        // Note: Name ascending, DateOfBirth descending is the format expected in the request for ordering
        private void ApplySort(ref IQueryable<Owner> owners, string orderByQueryString)
        {
            // some mandatory checks for owners and queryString
            if (!owners.Any())
                return;

            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                owners = owners.OrderBy(o => o.Name);
                return;
            }

            // splitting our query string to get the individual fields
            string[] orderParams = orderByQueryString.Trim().Split(',');
            
            // using reflection to prepare the list of PropertyInfo objects that represent the properties of our Owner class
            // We need them to be able to check if the field received through the query string really exists in the Owner class
            PropertyInfo[] propertyInfos = typeof(Owner).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var orderQueryBuilder = new StringBuilder();
            
            // through all the parameters 
            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                
                // check parameter's existence
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
                
                // If we don’t find such a property we skip the step in the foreach loop and go to the next parameter in the list
                if (objectProperty == null)
                    continue;

                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";
                
                // use the StringBuilder to build our query with each loop
                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}, ");
            }
            
            // removing excess commas 
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            
            //one last check to see if our query indeed has something in it
            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                owners = owners.OrderBy(x => x.Name);
                return;
            }

            owners = owners.OrderBy(orderQuery);
        }
    }
}
