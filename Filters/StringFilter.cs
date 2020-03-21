using System;
using System.Linq;
using System.Linq.Expressions;
using WebApp.Models;

namespace WebApp.Filters
{
    public class StringFilter : IFilter
    {
        private readonly string entityField;
        private readonly string value;

        public StringFilter(string entityType, string value)
        {
            this.entityField = entityType;
            this.value = value;
        }

        public IQueryable<Order> ApplyFilter(IQueryable<Order> query)
        {
            var param = Expression.Parameter(typeof(Order));
            var field = Expression.Property(param, entityField);
            var condition =
                Expression.Lambda<Func<Order, bool>>(
                    Expression.Equal(field, Expression.Constant(value, typeof(string))),
                    param
                );
            return query.Where(condition);
        }
    }
}