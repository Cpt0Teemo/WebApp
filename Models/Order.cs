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

        [Required(AllowEmptyStrings = false)]
        public string email { get; set; }

        public List<SubOrder> subOrders { get; set; } = new List<SubOrder>();

        public DateTime createdOn { get; private set; }

        public DateTime expectedDate { get; set; }

        public string comment { get; set; }

        public Nullable<DateTime> done { get; set; }

        [Timestamp]
        public byte[] timestamp { get; set; }

        public void SetupOrder()
        {
            SetNewOrderId();
            SetNewCreatedOn();
            SetSubOrderId();
            SetOrderForSubOrder();
        }

        private void SetNewOrderId()
        {
            if (orderId == null || orderId == Guid.Empty)
                orderId = Guid.NewGuid();
        }

        private void SetNewCreatedOn()
        {
            if (createdOn == null || createdOn == DateTime.MinValue)
                createdOn = DateTime.Now;
        }

        private void SetSubOrderId()
        {
            subOrders.ForEach(x => x.SetNewSubOrderId());
        }

        private void SetOrderForSubOrder()
        {
            subOrders.ForEach(x => x.order = this);
        }
    }
}
