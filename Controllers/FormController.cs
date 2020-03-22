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
        private readonly IRepository _repository;

        public FormController(IRepository repository)
        {
            _repository = repository;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] Order order)
        {
            var response = new JsonResponse();

            try
            {
                order.SetupOrder();
                await _repository.AddOrder(order);
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
                response = order.orderId
            });
        }

        [Route("Form/{id}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            return View("Confirmation",await _repository.GetOrder(id));
        }

    }


}