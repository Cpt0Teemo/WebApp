using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp
{
    public interface IRepository
    {
        Task AddOrder(Order order);
        Task<Order> GetOrder(Guid orderId);
        Task<List<Order>> GetOrders();
        Task<List<Order>> GetOrders(int take, int page);
        Task<OrderTableResponse> GetOrders(List<Filter> filters, int page, int take);
    }
}