using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using WebApp.Models;
using WebApp;
using WebApp.Validators;

namespace WebApp.Tests
{
    public class RepositoryTest
    {
        private readonly Mock<IValidator<Order>> _mockValidator;
        
        public RepositoryTest()
        {
            _mockValidator = new Mock<IValidator<Order>>();
        }

        [Fact]
        private async Task AddOrder_ShouldAddCorrectly()
        {
            var option = Option("AddOrder_ShouldAddCorrectly");
            var order = new Order();
            order.SetupOrder();
            _mockValidator
                .Setup(x => x.Validate(It.IsAny<Order>()))
                .ReturnsAsync(true);

            await InMemoryDb.AddOrderToInMemoryDatabase(option, _mockValidator.Object, order);
            
            var result = InMemoryDb.GetEntityFromInMemoryDatabase<Order>(option,  x => x.orderId == order.orderId);

            CompareOrders(order, result);
        }

       

        private DbContextOptions<OysterContext> Option(string name)
        {
            return new DbContextOptionsBuilder<OysterContext>()
                            .UseInMemoryDatabase(name)
                            .Options;
        }

        private void CompareOrders(Order expected, Order actual)
        {
            Assert.Equal(expected.orderId, actual.orderId);
            Assert.Equal(expected.email, actual.email);
            Assert.Equal(expected.name, actual.name);
            Assert.Equal(expected.createdOn, actual.createdOn);
            Assert.Equal(expected.expectedDate, actual.expectedDate);
            Assert.Equal(expected.comment, actual.comment);
            Assert.Equal(expected.done, actual.done);
            Assert.Equal(expected.timestamp, actual.timestamp);
        }
    }
}


