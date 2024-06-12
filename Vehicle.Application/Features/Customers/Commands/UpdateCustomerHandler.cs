using MediatR;
using FluentValidation;
//using CrudTest.Application.Common.Interfaces;
//using CrudTest.Domain.Entities;
using PhoneNumbers;
//using CrudTest.Domain.Entities;
//using CrudTest.Application.Commands;
using Vehicle.Application.Features.Customers.Commands;
using Vehicle.Domain.Entities.Concrete;
using Vehicle.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;
using Vehicle.Application.Features.Cars.Commands;
using Vehicle.Application.Features.Cars.Queries;
using Vehicle.Application.Common.Exceptions;

namespace CrudTest.Application.Handlers
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, int>
    {
        private readonly ICustomerRepository _context;
        private readonly IValidator<Customer> _validator;
        private readonly ILogger<CreateCustomerHandler> _logger;

        public UpdateCustomerHandler(ICustomerRepository context, IValidator<Customer> validator, ILogger<CreateCustomerHandler> logger)
        {
            _context = context;
            _validator = validator;
            _logger = logger;
        }

        public async Task<int> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.GetByIdAsync(request.Id);

            if (customer == null)
            {
                throw new NotFoundException(nameof(Customer), request.Id);
            }

            var phoneUtil = PhoneNumberUtil.GetInstance();
            var numberProto = phoneUtil.Parse(request.PhoneNumber, "US");
            var formattedPhoneNumber = phoneUtil.Format(numberProto, PhoneNumberFormat.E164);

            if (!phoneUtil.IsValidNumber(numberProto))
            {
                throw new ValidationException("Invalid phone number");
            }

            if (!await _context.CheckUniquenessBYEmail(request.Email))
            {
                throw new ValidationException("Email must be unique");
            }

           
            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.DateOfBirth = request.DateOfBirth;
            customer.PhoneNumber = numberProto.NationalNumber.ToString();
            customer.Email = request.Email;
            customer.BankAccountNumber = request.BankAccountNumber;

          
                       
            var result = await _context.AddAsync(customer);
            _logger.LogInformation($"{result.Id}");
            return result.Id;
        }
    }
}
