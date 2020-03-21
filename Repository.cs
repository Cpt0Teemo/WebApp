using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Validators;
using WebApp.Filters;

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
            if (!await _orderValidator.Validate(order))
                throw new InvalidOrderException("Missing suborders from order");
            
            _context.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetOrder(Guid orderId)
        {
            return await Orders
                .FirstOrDefaultAsync(x => x.orderId == orderId);
        }

        public async Task<List<Order>> GetOrders()
        {
            return await Orders
                .OrderByDescending(x => x.expectedDate)
                .ToListAsync();
        }
        
        public async Task<List<Order>> GetOrders(int page, int take)
        {
            return await Orders
                .OrderByDescending(x => x.expectedDate)
                .Skip((page - 1) * take)
                .Take(take)
                .ToListAsync();
        }

        public async Task<OrderTableResponse> GetOrders(List<IFilter> filters, int page, int take)
        {
            var query = AddFiltersToQuery(Orders, filters);

            var ordersAsync = query
                .OrderByDescending(x => x.expectedDate)
                .Skip((page - 1) * take)
                .Take(take)
                .ToListAsync();
            var countAsync = query.CountAsync();

            var orderTable = new OrderTableResponse
            {
                orders = await ordersAsync,
                totalCount = await countAsync,
                pages = (int) Math.Ceiling(await countAsync / (decimal) take)
            };

            return orderTable;
        }

        private IQueryable<Order> AddFiltersToQuery(IQueryable<Order> query, List<IFilter> filters)
        {
            foreach (var filter in filters)
            {
                query = filter.ApplyFilter(query);
            }

            return query;
        }
    }
} 
