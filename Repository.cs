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
    }
}
