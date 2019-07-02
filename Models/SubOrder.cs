using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class SubOrder
    {
        public Guid subOrderId { get; private set; }

        [Required]
        public Order order { get; set; }

        public int quantity { get; set; }

        public OysterType.OysterTypes oysterType { get; set; }

        public void setNewSubOrderId()
        {
            if (subOrderId == null || subOrderId == Guid.Empty)
                subOrderId = Guid.NewGuid();
        }
    }
}
