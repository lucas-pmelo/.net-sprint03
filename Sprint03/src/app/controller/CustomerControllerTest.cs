using Microsoft.AspNetCore.Mvc;
using Moq;
using Sprint03.adapter.input.dto;
using Sprint03.app.controller;
using Sprint03.domain.model;
using Sprint03.infra.exception;
using Xunit;

namespace Sprint03.Tests
{
    public class CustomerControllerTests
    {
        private readonly Mock<ICustomerAdapter> _mockCustomerAdapter;
        private readonly CustomerController _controller;

        public CustomerControllerTests()
        {
            _mockCustomerAdapter = new Mock<ICustomerAdapter>();
            _controller = new CustomerController(_mockCustomerAdapter.Object);
        }

        [Fact]
        public void FindById_ReturnsOkResult_WithCustomer()
        {
            var customerId = "1";
            var customer = new Customer { Id = customerId, Name = "John Doe" };
            _mockCustomerAdapter.Setup(x => x.FindById(customerId)).Returns(customer);

            var result = _controller.FindById(customerId) as ActionResult<Customer>;

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCustomer = Assert.IsType<Customer>(okResult.Value);
            Assert.Equal(customerId, returnedCustomer.Id);
        }

        [Fact]
        public void FindById_ReturnsNotFoundResult_WhenCustomerNotFound()
        {
            var customerId = "1";
            _mockCustomerAdapter.Setup(x => x.FindById(customerId)).Throws(new NotFoundException("Customer not found"));

            var result = _controller.FindById(customerId) as ActionResult<Customer>;

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("Customer not found", (notFoundResult.Value as dynamic).message);
        }

        [Fact]
        public void Create_ReturnsCreatedAtActionResult_WhenSuccessful()
        {
            var customer = new Customer { Id = "1", Name = "John Doe" };
            _mockCustomerAdapter.Setup(x => x.Create(customer)).Verifiable();

            var result = _controller.Create(customer) as ActionResult<Customer>;

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedCustomer = Assert.IsType<Customer>(createdResult.Value);
            Assert.Equal(customer.Id, returnedCustomer.Id);
        }

        [Fact]
        public void Create_ReturnsBadRequestResult_WhenInvalidExceptionThrown()
        {
            var customer = new Customer { Id = "1", Name = "John Doe" };
            _mockCustomerAdapter.Setup(x => x.Create(customer)).Throws(new InvalidException("Invalid data"));

            var result = _controller.Create(customer) as ActionResult<Customer>;

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Invalid data", (badRequestResult.Value as dynamic).message);
        }

        [Fact]
        public void Update_ReturnsOkResult_WithUpdatedCustomer()
        {
            var customerId = "1";
            var customer = new Customer { Id = customerId, Name = "John Doe" };
            var updatedCustomer = new Customer { Id = customerId, Name = "Jane Doe" };
            _mockCustomerAdapter.Setup(x => x.Update(customerId, customer)).Returns(updatedCustomer);

            var result = _controller.Update(customerId, customer) as ActionResult<Customer>;

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCustomer = Assert.IsType<Customer>(okResult.Value);
            Assert.Equal("Jane Doe", returnedCustomer.Name);
        }

        [Fact]
        public void Delete_ReturnsNoContentResult_WhenSuccessful()
        {
            var customerId = "1";
            _mockCustomerAdapter.Setup(x => x.Delete(customerId)).Verifiable();

            var result = _controller.Delete(customerId) as IActionResult;

            Assert.IsType<NoContentResult>(result);
        }
    }
}
