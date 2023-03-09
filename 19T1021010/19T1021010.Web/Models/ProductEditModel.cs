using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021010.DomainModels;

namespace _19T1021010.Web.Models
{
    public class ProductEditModel : Product
    {
        public Product Product { get; set; }
        public List<ProductAttribute> Attributes { get; set; }
        public List<ProductPhoto> Photos { get; set; }
    }
}