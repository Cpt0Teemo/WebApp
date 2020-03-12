using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Validators;

namespace WebApp
{
    public class Repository : IRepository
    {
        private readonly OysterContext _context;
        private readonly IValidator<Order> _orderValidator;
        
        private IQueryable<Order> Orders => _context.Orders.Include(x => x.subOrders);

        public Repository(OysterContext context, IValidator<Order> orderValidator)
        {
            _context = context;
            _orderValidator = orderValidator;
        }

        public async Task AddOrder(Order order)
        {
            if (await _orderValidator.Validate(order))
                throw new InvalidOrderException("Missing suborders from order");
            await _context.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetOrder(Guid orderId)
        {
            return await _context.Orders
                .Include(x => x.subOrders)
                .FirstOrDefaultAsync(x => x.orderId == orderId);
        }

        public async Task<List<Order>> GetOrders()
        {
            return await Orders.ToListAsync();
        }
        public async Task<List<Order>> GetOrders(int take, int page)
        {
            return await Orders
                .Skip((page - 1) * take)
                .Take(take)
                .ToListAsync();
        }

        public async Task<OrderTableResponse> GetOrders(List<Filter> filters, int page, int take)
        {
            var query = AddFiltersToQuery(Orders, filters);

            var ordersAsync = query.Skip((page - 1) * take).Take(take).ToListAsync();
            var countAsync = query.CountAsync();

            var orderTable = new OrderTableResponse
            {
                orders = await ordersAsync,
                totalCount = await countAsync,
                pages = (int) Math.Ceiling(await countAsync / (decimal) take)
            };

            return orderTable;
        }

        private IQueryable<Order> AddFiltersToQuery(IQueryable<Order> query, List<Filter> filters)
        {
            foreach (var filter in filters)
            {
                switch (filter.filterEntity)
                {
                    case Filter.FilterEntity.orderId:
                        query.Where(x => x.orderId.ToString() == filter.value);
                        break;
                    case Filter.FilterEntity.email:
                        query.Where(x => x.email == filter.value);
                        break;
                    case Filter.FilterEntity.name:
                        query.Where(x => x.name == filter.value);
                        break;
                    case Filter.FilterEntity.createdOn:
                        query.Where(x => x.createdOn == DateTime.Parse(filter.value));
                        break;
                    case Filter.FilterEntity.excpectedDate:
                        query.Where(x => x.expectedDate == DateTime.Parse(filter.value));
                        break;
                    case Filter.FilterEntity.comment:
                        query.Where(x => x.comment == filter.value);
                        break;
                    case Filter.FilterEntity.done:
                        query.Where(x => x.done == DateTime.Parse(filter.value));
                        break;
                }
            }

            return query;
        }
    }
} 
