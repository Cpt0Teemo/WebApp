using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public List<Order> getOrders()
        {
            _context.Orders.Where()
        }

        public IQueryable<Order> addFiltersToQuery(IQueryable<Order> query, List<Filter> filters)
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
        }
    }
}
