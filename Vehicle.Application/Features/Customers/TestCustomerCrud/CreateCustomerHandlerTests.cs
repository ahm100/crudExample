using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FluentValidation;
using FluentValidation.Results;
using CrudTest.Application.Handlers;
using Vehicle.Application.Features.Customers.Commands;
using Vehicle.Domain.Entities.Concrete;
using Microsoft.Extensions.Logging;
using Vehicle.Application.Contracts.Persistence;

namespace Vehicle.Application.Features.Customers.TestCustomerCrud
{
    public class CreateCustomerHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCustomer_ReturnsCustomerId()
        {
            // Arrange
            var validatorMock = new Mock<IValidator<Customer>>();
            var customerRepository = new Mock<ICustomerRepository>();
            var customerValidator = new Mock<IValidator<Customer>>();
            var loggerMock = new Mock<ILogger<CreateCustomerHandler>>();

            // Create a mock logger
            validatorMock.Setup(v => v.Validate(It.IsAny<Customer>())).Returns(new ValidationResult());

            // Set up logger behavior (optional)
            // loggerMock.Setup(l => l.Log(...));

            // Create a mock customer ID (positive integer)
            //var customerId = 4; // shuould be the real Id in db

            // Set up customer repository behavior
            //customerRepository.Setup(repo => repo.AddAsync(It.IsAny<Customer>())).ReturnsAsync(new CustomerResult { Id = customerId });
            //It.IsAny<Customer>(): This expression represents any instance of the Customer class. When you use it in a Moq setup, it allows the mocked method to accept any Customer object as an argument.

            var handler = new CreateCustomerHandler(customerRepository.Object, validatorMock.Object, loggerMock.Object);

            var command = new CreateCustomerCommand
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumber = "+1234567890",
                Email = "john.doe@example.com",
                BankAccountNumber = "123456789"
            };

            
            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result > 0); // Verify that the result is a positive integer
        }
    }

    internal class CustomerResult : Customer
    {
        public int Id { get; set; }
    }
}
