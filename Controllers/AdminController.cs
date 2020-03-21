using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Filters;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRepository _repository;
        public AdminController(IRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<IActionResult> Index()
        {
            var orders = await _repository.GetOrders(new List<IFilter>(), 1, 10);
            return View("Table", orders);
        }
        
        [HttpGet]
        public async Task<IActionResult> getOrderTable(int page, int take)
        {
            var orders = await _repository.GetOrders(new List<IFilter>(), page, take);
            return PartialView("Table", orders);
        }
        
        [HttpGet]
        public async Task<IActionResult> getFilteredOrderTable(List<IFilter> filters, int take, int page)
        {
            var orders = await _repository.GetOrders(filters ,take, page);
            return PartialView("Table", orders);
        }

    }
}