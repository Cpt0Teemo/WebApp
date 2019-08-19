using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AdminController : Controller
    {
        private OysterContext _context;
        private Repository _repository;
        public AdminController(OysterContext context)
        {
            _context = context;
            _repository = new Repository(_context);
        }
        
        public IActionResult Index()
        {
            var orders = _repository.getOrders(new List<Filter>(), 10, 1);
            return View("Table", orders);
        }
        
        [HttpGet]
        public IActionResult getOrderTable(int take, int page)
        {
            var orders = _repository.getOrders(new List<Filter>(), 20, 1);
            return PartialView("Table", orders);
        }
        
        [HttpGet]
        public IActionResult getFilteredOrderTable(List<Filter> filters, int take, int page)
        {
            var orders = _repository.getOrders(filters ,take, page);
            return PartialView("Table", orders);
        }

    }
}