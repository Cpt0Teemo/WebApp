using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class SubOrder
    {
        public Order order { get; }

        public int quantity { get; private set; }

        public OysterType oysterType { get; private set; }

    }
}
