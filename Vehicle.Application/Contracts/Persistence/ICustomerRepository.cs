using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Application.Features.Cars.Queries;
using Vehicle.Domain.Entities.Concrete;

namespace Vehicle.Application.Contracts.Persistence
{
    public interface ICustomerRepository : IAsyncRepository<Customer>
    {
        Task<bool> CheckUniqueness(string firstName, string lastName,DateTime dateOfBirth);
        Task<bool> CheckUniquenessBYEmail(string email);
    }
}
