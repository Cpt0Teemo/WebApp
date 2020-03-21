using System;
using System.Linq;
using System.Linq.Expressions;
using WebApp.Models;

namespace WebApp.Filters
{
    public class ApproximateStringFilter : IFilter
    {
        private readonly string entityField;
        private readonly string value;

        public ApproximateStringFilter(string entityType, string value)
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
                    Expression.LessThan(field, Expression.Constant(value, typeof(string))),
                    param
                );
            return query.Where(condition);
            /*var field = typeof(Order).GetProperty(entityField);
            if (field.GetType() != typeof(string))
                throw new InvalidCastException(field.Name + " is not of type string");
            return query.Where(x => ((string) field.GetValue(x)).Contains(value));*/
        }
    }
}