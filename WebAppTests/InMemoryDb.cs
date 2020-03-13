using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Tests
{
    public static class InMemoryDb
    {
        private async Task AddOrderToInMemoryDatabase(DbContextOptions<OysterContext> option, Order order)
        {
            using (var context = new OysterContext(option))
            {
                var repository = new Repository(context, _mockValidator.Object);
                await repository.AddOrder(order);
            }
        }

        private T GetEntityFromInMemoryDatabase<T>(DbContextOptions<OysterContext> option, Func<T, bool> predicate)
            where T : class
        {
            using (var context = new OysterContext(option))
            {
                if (typeof(T) == typeof(Order))
                    return (T) (object) context.Orders.First((Func<Order, bool>) predicate);
                else if (typeof(T) == typeof(SubOrder))
                    return (T) (object) context.SubOrders.First((Func<SubOrder, bool>) predicate);
                else
                    throw new Exception("Entity type not supported");
            }
        }
    }
}