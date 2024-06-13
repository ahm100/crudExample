using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle.Application.Features.Customers.Queries
{
    internal class ListCustomersQuery : IRequest<List<CustomerVM>>
    {
       
    }
}
