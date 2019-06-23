using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Order
    {
        public Guid orderId { get; private set; }

        [Required(AllowEmptyStrings = false)]
        public string name { get; set; }

        public List<SubOrder> subOrders { get; set; }

        public DateTime createdOn { get; set; }

        public DateTime expectedDate { get; set; }

        public string comment { get; set; }

        public Nullable<DateTime> done { get; set; }

        [Timestamp]
        public byte[] timestamp { get; set; }
    }
}
