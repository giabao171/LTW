using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021010.DomainModels;

namespace _19T1021010.Web.Models
{
    public class OrderDetailsModel : Order
    {
        public Order Order {get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}