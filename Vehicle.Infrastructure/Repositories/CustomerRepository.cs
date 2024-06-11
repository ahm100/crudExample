using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Application.Contracts.Persistence;
//using Vehicle.Application.Features.Customers.Queries;
using Vehicle.Domain.Entities.Concrete;
using Vehicle.Infrastructure.Persistence;

namespace Vehicle.Infrastructure.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> CheckUniqueness(string firstName, string lastName, DateTime dateOfBirth)
        {
            var result =  _dbContext.Customers
                .Any(c => c.FirstName == firstName && c.LastName == lastName && c.DateOfBirth == dateOfBirth);
            return !result;

        }

        public async Task<bool> CheckUniquenessBYEmail(string email)
        {
            var result = _dbContext.Customers
               .Any(c => c.Email == email);
            return !result;
        }

        
    }
}
