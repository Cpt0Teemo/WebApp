using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp
{
    public class Repository
    {
        public OysterContext _context;
        
        public Repository(OysterContext context)
        {
            _context = context;
        }

        public void addOrder(Order order)
        {
            _context.Add(order);
            _context.SaveChanges();
        }

        public List<Order> getOrders(int take, int page)
        {
            return _context.Orders.Skip((page - 1) * take).Take(take).ToList();
        }

        public OrderTableReponse getOrders(List<Filter> filters, int take, int page)
        {
            var query = _context.Orders.Include(x => x.subOrders).AsQueryable();
            query = addFiltersToQuery(query, filters);
            
            var ordersAsync =  query.Skip((page - 1) * take).Take(take).ToListAsync();
            var countAsync =  query.CountAsync();
            
            var orderTable = new OrderTableReponse
            {
                orders = ordersAsync.Result,
                totalCount = countAsync.Result,
                pages = (int)Math.Ceiling(countAsync.Result / (decimal)take)
            };
            
            return orderTable;
        }

        private IQueryable<Order> addFiltersToQuery(IQueryable<Order> query, List<Filter> filters)
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
