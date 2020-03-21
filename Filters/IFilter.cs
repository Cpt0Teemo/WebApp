using System.Linq;
using WebApp.Models;

namespace WebApp.Filters
{
    public interface IFilter
    {
        IQueryable<Order> ApplyFilter(IQueryable<Order> query);
    }
}