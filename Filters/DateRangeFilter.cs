using System;
using System.Linq;
using System.Linq.Expressions;
using WebApp.Models;

namespace WebApp.Filters
{
    public class DateRangeFilter : IFilter
    {
        private readonly string entityField;
        private readonly DateTime minDateTime;
        private readonly DateTime maxDateTime;

        public DateRangeFilter(string entityField, DateTime minDateTime, DateTime maxDateTime)
        {
            this.entityField = entityField;
            this.minDateTime = minDateTime;
            this.maxDateTime = maxDateTime;
        }

        public IQueryable<Order> ApplyFilter(IQueryable<Order> query)
        {
            var param = Expression.Parameter(typeof(Order));
            var field = Expression.Property(param, entityField);
            var condition =
                Expression.Lambda<Func<Order, bool>>(
                    Expression.And(
                        Expression.LessThanOrEqual(Expression.Constant(minDateTime, typeof(DateTime)), field),
                        Expression.LessThanOrEqual(field, Expression.Constant(maxDateTime, typeof(DateTime)))
                    ),
                    param
                );
            return query.Where(condition);
        }
    }
}