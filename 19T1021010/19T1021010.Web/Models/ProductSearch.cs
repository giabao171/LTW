using _19T1021010.DomainModels;
using _19T1021201.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021010.Web.Models
{
    public class ProductSearch : BasePaginationResultProduct
    {
        public List<Product> Data { get; set; }
    }
}