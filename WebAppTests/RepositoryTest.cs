using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using WebApp.Models;
using WebApp.Filters;
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
            var order = _orderHelper.CreateOrderWithSubOrders(new List<SubOrder>() {subOrder});

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
            var option = Option("GetOrder_ShouldGetCorrectly");
            var subOrder = _orderHelper.CreateSubOrderWithTypeAndQuantity(OysterType.OysterTypes.Arcachon_3, 10);
            var order = _orderHelper.CreateOrderWithSubOrders(new List<SubOrder>() {subOrder});

            _mockValidator
                .Setup(x => x.Validate(It.IsAny<Order>()))
                .ReturnsAsync(true);

            var mockRepository = new InMemoryDb(option, _mockValidator.Object);
            await mockRepository.AddOrder(order);

            var result = await mockRepository.GetOrder(order.orderId);

            _orderHelper.CompareOrders(order, result);
        }

        [Fact]
        private async Task GetOrder_ShouldReturnNullWithWrongId()
        {
            var option = Option("GetOrder_ShouldGetCorrectly");
            var subOrder = _orderHelper.CreateSubOrderWithTypeAndQuantity(OysterType.OysterTypes.Arcachon_3, 10);
            var order = _orderHelper.CreateOrderWithSubOrders(new List<SubOrder>() {subOrder});

            _mockValidator
                .Setup(x => x.Validate(It.IsAny<Order>()))
                .ReturnsAsync(true);

            var mockRepository = new InMemoryDb(option, _mockValidator.Object);
            await mockRepository.AddOrder(order);

            var result = await mockRepository.GetOrder(Guid.NewGuid());

            Assert.Null(result);
        }

        [Fact]
        private async Task GetOrders_ShouldReturnAllOrders()
        {
            var option = Option("GetOrders_ShouldReturnAllOrders");
            var expected = CreateTwoBasicOrdersOrderedByExpectedDate();

            _mockValidator
                .Setup(x => x.Validate(It.IsAny<Order>()))
                .ReturnsAsync(true);

            var mockRepository = new InMemoryDb(option, _mockValidator.Object);
            var tasks = new List<Task>();
            foreach (var order in expected)
            {
                tasks.Add(mockRepository.AddOrder(order));
            }

            await Task.WhenAll(tasks);

            var result = await mockRepository.GetOrders();

            foreach (var zippedList in expected.Zip(result))
            {
                _orderHelper.CompareOrders(zippedList.First, zippedList.Second);
            }
        }

        [Fact]
        private async Task GetOrderWithTake_ShouldReturnOrderedListSizeTake()
        {
            var option = Option("GetOrderWithTake_ShouldReturnOrderedListSizeTake");
            var expected = CreateTwoBasicOrdersOrderedByExpectedDate();

            _mockValidator
                .Setup(x => x.Validate(It.IsAny<Order>()))
                .ReturnsAsync(true);

            var mockRepository = new InMemoryDb(option, _mockValidator.Object);
            var tasks = new List<Task>();
            foreach (var order in expected)
            {
                tasks.Add(mockRepository.AddOrder(order));
            }

            await Task.WhenAll(tasks);

            var result = await mockRepository.GetOrders(1, 1);

            Assert.Single(result.orders);
            Assert.Equal(expected.Count(), result.totalCount);
            _orderHelper.CompareOrders(expected.First(), result.orders.First());
        }

        [Fact]
        private async Task GetOrdersWithPage_ShouldSkip()
        {
            var option = Option("GetOrderWithTake_ShouldReturnOrderedListSizeTake");
            var expected = CreateTwoBasicOrdersOrderedByExpectedDate();

            _mockValidator
                .Setup(x => x.Validate(It.IsAny<Order>()))
                .ReturnsAsync(true);

            var mockRepository = new InMemoryDb(option, _mockValidator.Object);
            var tasks = new List<Task>();
            foreach (var order in expected)
            {
                tasks.Add(mockRepository.AddOrder(order));
            }

            await Task.WhenAll(tasks);

            var result = await mockRepository.GetOrders(2, 1);

            Assert.Single(result.orders);
            Assert.Equal(expected.Count(), result.totalCount);
            _orderHelper.CompareOrders(expected.Last(), result.orders.First());
        }

        [Fact]
        private async Task GerOrdersWithDateFilter_ShouldReturnFilteredList()
        {
            var option = Option("GetOrdersWithDateFilter_ShouldReturnFilteredList");
            var expected = CreateThreeBasicOrdersOrderedByExpectedDate();

            _mockValidator
                .Setup(x => x.Validate(It.IsAny<Order>()))
                .ReturnsAsync(true);

            var mockRepository = new InMemoryDb(option, _mockValidator.Object);
            var tasks = new List<Task>();
            foreach (var order in expected)
            {
                tasks.Add(mockRepository.AddOrder(order));
            }

            await Task.WhenAll(tasks);
            
            var dateFilter = new DateRangeFilter("expectedDate", DateTime.Parse("01/01/2019"), DateTime.Parse("01/01/2021"));
            var filters = new List<IFilter>() {dateFilter};

            var result = await mockRepository.GetOrders(filters, 1, 10);

            Assert.Single(result.orders);
            Assert.Equal(1, result.totalCount);
            _orderHelper.CompareOrders(expected.Skip(1).First(), result.orders.First());
        }


        private DbContextOptions<OysterContext> Option(string name)
        {
            return new DbContextOptionsBuilder<OysterContext>()
                .UseInMemoryDatabase(name)
                .Options;
        }

        private IOrderedEnumerable<Order> CreateTwoBasicOrdersOrderedByExpectedDate()
        {
            var subOrder1 = _orderHelper.CreateSubOrderWithTypeAndQuantity(OysterType.OysterTypes.Arcachon_3, 10);
            var subOrder2 = _orderHelper.CreateSubOrderWithTypeAndQuantity(OysterType.OysterTypes.Arguin_2, 20);
            var order1 = _orderHelper.CreateOrderWithSubOrders(new List<SubOrder>() {subOrder1});
            var order2 = _orderHelper.CreateOrderWithSubOrders(new List<SubOrder>() {subOrder2});
            order1.expectedDate = DateTime.MinValue;
            order2.expectedDate = DateTime.MaxValue;
            var expected = new List<Order>() {order1, order2}.OrderByDescending(x => x.expectedDate);
            return expected;
        }

        private IOrderedEnumerable<Order> CreateThreeBasicOrdersOrderedByExpectedDate()
        {
            var subOrder1 = _orderHelper.CreateSubOrderWithTypeAndQuantity(OysterType.OysterTypes.Arcachon_3, 10);
            var subOrder2 = _orderHelper.CreateSubOrderWithTypeAndQuantity(OysterType.OysterTypes.Arguin_3, 5);
            var subOrder3 = _orderHelper.CreateSubOrderWithTypeAndQuantity(OysterType.OysterTypes.Arguin_2, 20);
            var order1 = _orderHelper.CreateOrderWithSubOrders(new List<SubOrder>() {subOrder1});
            var order2 = _orderHelper.CreateOrderWithSubOrders(new List<SubOrder>() {subOrder2});
            var order3 = _orderHelper.CreateOrderWithSubOrders(new List<SubOrder>() {subOrder3});
            order1.expectedDate = DateTime.MinValue;
            order2.expectedDate = DateTime.Parse("01/01/2020");
            order3.expectedDate = DateTime.MaxValue;
            var expected = new List<Order>() {order1, order2, order3}.OrderByDescending(x => x.expectedDate);
            return expected;
        }
    }
}