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
        private Repository _repository;

        public FormController(OysterContext context)
        {
            _repository = new Repository(context);
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
                _repository.addOrder(order);
            } catch (Exception e)
            {
                response.success = false;
                response.message = e.Message;
                return Json(new JsonResponse {
                    success = false,
                    message = e.Message
                });

            }

            return Json(new JsonResponse
            {
                success = true,
                orderId = order.orderId
            });
        }

        [Route("Form/{id}")]
        public IActionResult GetOrder(Guid id)
        {
            return View("Confirmation",_repository.getOrder(id));
        }

    }


}