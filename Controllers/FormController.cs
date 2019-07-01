using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class FormController : Controller
    {
        private OysterContext _context;

        public FormController(OysterContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddOrder([FromBody] Order order)
        {
            order.setupOrder();
            _context.Add(order);
            _context.SaveChanges();
            return View("Confirmation");
        }

    }


}