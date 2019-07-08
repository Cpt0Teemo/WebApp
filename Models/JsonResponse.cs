using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class JsonResponse
    {
        public bool success { get; set; }

        public Guid orderId { get; set; }

        public string message { get; set; }
    }
}
