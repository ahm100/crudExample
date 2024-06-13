using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle.Application.Features.Cars.Queries
{
    public class ListCustomerQuery : IRequest<List<CarVM>>
    {
        public string Color { get; set; }
        public ListCustomerQuery(string colorCar)
        {
            Color = colorCar;
        }
    }
}
