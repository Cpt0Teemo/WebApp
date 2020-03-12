using System;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Validators
{
    public class OrderValidator : IValidator<Order>
    {
        public async Task<bool> Validate(Order order)
        {
            //TODO Add validation for orders
            return true;
        }
    }

    public class InvalidOrderException : Exception
    {
        public InvalidOrderException(string message) : base(message)
        {
        }
    }
}