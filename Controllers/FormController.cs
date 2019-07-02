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
            var response = new JsonResponse();

            try
            {
                order.setupOrder();
                _context.Add(order);
                _context.SaveChanges();
            } catch (Exception e)
            {
                response.success = false;
                response.message = e.Message;
                return Json(response);

            }

            response.success = true;
            response.order = order;

            return Json(response);
        }

    }


}