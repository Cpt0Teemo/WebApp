using System.Collections.Generic;
using System.Linq;
using WebApp.Models;
using Xunit;

namespace WebApp.Tests
{
    public class OrderHelper
    {
        public Order CreateOrder()
        {
            var order = new Order();
            order.SetupOrder();
            return order;
        }

        public Order CreateOrderWithSubOrders(List<SubOrder> subOrders)
        {
            var order = new Order();
            order.subOrders = subOrders;
            order.SetupOrder();
            return order;
        }

        public SubOrder CreateSubOrderWithTypeAndQuantity(OysterType.OysterTypes oysterType, int quantity)
        {
            var subOrder = new SubOrder();
            subOrder.quantity = quantity;
            subOrder.oysterType = oysterType;
            return subOrder;
        }
        public void CompareOrders(Order expected, Order actual)
        {
            Assert.Equal(expected.orderId, actual.orderId);
            Assert.Equal(expected.email, actual.email);
            Assert.Equal(expected.name, actual.name);
            Assert.Equal(expected.createdOn, actual.createdOn);
            Assert.Equal(expected.expectedDate, actual.expectedDate);
            Assert.Equal(expected.comment, actual.comment);
            Assert.Equal(expected.done, actual.done);
            Assert.Equal(expected.timestamp, actual.timestamp);
            
            Assert.Equal(expected.subOrders.Count, actual.subOrders.Count);
            foreach (var value in Enumerable.Zip(expected.subOrders, actual.subOrders))
            {
                CompareSubOrders(value.First, value.Second);
            }
        }

        public void CompareSubOrders(SubOrder expected, SubOrder actual)
        {
            Assert.Equal(expected.subOrderId, actual.subOrderId);
            Assert.Equal(expected.quantity, actual.quantity);
            Assert.Equal(expected.oysterType, actual.oysterType);
        }
    }
}