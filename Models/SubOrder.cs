using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class SubOrder
    {
        public Guid subOrderId { get; set; }

        [Required]
        public Order order { get; }

        public int quantity { get; private set; }

        public OysterType.OysterTypes oysterType { get; private set; }

    }
}
