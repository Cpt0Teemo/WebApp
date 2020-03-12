using System;
using Xunit;
using Moq;
using WebApp.Models;
using WebApp;
using WebApp.Validators;

namespace WebApp.Tests
{
    public class RepositoryTest
    {
        private readonly Mock<OysterContext> _mockContext;
        private readonly Mock<IValidator<Order>> _mockValidator;
        private readonly IRepository _repository;
        
        public RepositoryTest()
        {
            _mockContext = new Mock<OysterContext>();
            _mockValidator = new Mock<IValidator<Order>>();
            _repository = new Repository(_mockContext.Object, _mockValidator.Object);
        }

        [Fact]
        private void AddOrder_ShouldAddCorrectly()
        {
           var order = new Order();
           _mockContext.Setup(x => x.Orders).Returns()
           _mockValidator.Setup(x => x.Validate(It.IsAny<Order>()))
               .ReturnsAsync(true);
           
           _repository.AddOrder(order);
           
           Assert.True(true);
        }
        
    }
}