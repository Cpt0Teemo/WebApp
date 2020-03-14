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
        private readonly OrderHelper _orderHelper;

        public RepositoryTest()
        {
            _mockValidator = new Mock<IValidator<Order>>();
            _orderHelper = new OrderHelper();
        }

        [Fact]
        private async Task AddOrder_ShouldAddCorrectly()
        {
            var option = Option("AddOrder_ShouldAddCorrectly");
            var subOrder = _orderHelper.CreateSubOrderWithTypeAndQuantity(OysterType.OysterTypes.Arcachon_3, 10);
            var order = _orderHelper.CreateOrderWithSubOrders(new List<SubOrder>(){subOrder});
            
            _mockValidator
                .Setup(x => x.Validate(It.IsAny<Order>()))
                .ReturnsAsync(true);

            var mockRepository = new InMemoryDb(option, _mockValidator.Object);
            await mockRepository.AddOrder(order);

            var result = InMemoryDb.GetEntityFromInMemoryDatabase<Order>(option, x => x.orderId == order.orderId);

            _orderHelper.CompareOrders(order, result);
        }

        [Fact]
        private async Task AddOrder_ShouldFailIfOrderNotValid()
        {
            var option = Option("AddOrder_ShouldFailIfOrderNotValid");
            var order = new Order();
            order.SetupOrder();
            _mockValidator
                .Setup(x => x.Validate(It.IsAny<Order>()))
                .ReturnsAsync(false);

            try
            {
                var mockRepository = new InMemoryDb(option, _mockValidator.Object);
                await mockRepository.AddOrder(order);
                Assert.True(false);
            }
            catch (InvalidOrderException e)
            {
                Assert.True(true);
            }
        }

        [Fact]
        private async Task GetOrder_ShouldGetCorrectly()
        {
            
        }
        
        private DbContextOptions<OysterContext> Option(string name)
        {
            return new DbContextOptionsBuilder<OysterContext>()
                .UseInMemoryDatabase(name)
                .Options;
        }
    }
}


