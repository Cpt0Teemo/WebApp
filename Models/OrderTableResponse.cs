using System.Collections.Generic;

namespace WebApp.Models
{
    public class OrderTableResponse
    {
        public List<Order> orders { get; set; }
        
        public int totalCount { get; set; }
        
        public int pages { get; set; }
    }
}