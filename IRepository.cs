using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Filters;
using WebApp.Models;

namespace WebApp
{
    public interface IRepository
    {
        Task AddOrder(Order order);
        Task<Order> GetOrder(Guid orderId);
        Task<List<Order>> GetOrders();
        Task<OrderTableResponse> GetOrders(int take, int page);
        Task<OrderTableResponse> GetOrders(List<IFilter> filters, int page, int take);
    }
}