using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021010.DomainModels;
using _19T1021201.Web.Models;

namespace _19T1021010.Web.Models
{
    public class OrderSearch : BasePaginationResultOrder
    {
        public List<Order> Data { get; set; }
    }

}