using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Validators;

namespace WebApp.Tests
{
    public class InMemoryDb : IRepository
    {
        private readonly DbContextOptions<OysterContext> _option;
        private readonly IValidator<Order> _validator;

        public InMemoryDb(DbContextOptions<OysterContext> option, IValidator<Order> validator)
        {
            _option = option;
            _validator = validator;
        }
        
        public async Task AddOrder(Order order)
        {
            using var context = new OysterContext(_option);
            var repository = new Repository(context, _validator);
            await repository.AddOrder(order);
        }

        public async Task<Order> GetOrder(Guid orderId)
        {
            using var context = new OysterContext(_option);
            var repository = new Repository(context, _validator);
            return await repository.GetOrder(orderId);
        }

        public async Task<List<Order>> GetOrders()
        {
            using var context = new OysterContext(_option);
            var repository = new Repository(context, _validator);
            return await repository.GetOrders();
        }

        public async Task<List<Order>> GetOrders(int take, int page)
        {
            using var context = new OysterContext(_option);
            var repository = new Repository(context, _validator);
            return await repository.GetOrders(take, page); 
        }

        public async Task<OrderTableResponse> GetOrders(List<Filter> filters, int page, int take)
        {
            using var context = new OysterContext(_option);
            var repository = new Repository(context, _validator);
            return await repository.GetOrders(filters, page, take);
        }

        public static T GetEntityFromInMemoryDatabase<T>(DbContextOptions<OysterContext> option,
            Func<T, bool> predicate)
            where T : class
        {
            using var context = new OysterContext(option);
            if (typeof(T) == typeof(Order))
                return (T) (object) context.Orders
                    .Include(x => x.subOrders)
                    .First((Func<Order, bool>) predicate);

            else if (typeof(T) == typeof(SubOrder))
                return (T) (object) context.SubOrders.First((Func<SubOrder, bool>) predicate);

            else
                throw new Exception("Entity type not supported");
        }

        public static T GetEntityListFromInMemoryDatabase<T>(DbContextOptions<OysterContext> option,
            Func<T, bool> predicate)
            where T : class
        {
            using var context = new OysterContext(option);
            if (typeof(T) == typeof(Order))
                return (T) (object) context.Orders
                    .Include(x => x.subOrders)
                    .First((Func<Order, bool>) predicate);

            else if (typeof(T) == typeof(SubOrder))
                return (T) (object) context.SubOrders.First((Func<SubOrder, bool>) predicate);

            else
                throw new Exception("Entity type not supported");
        }
    }
}