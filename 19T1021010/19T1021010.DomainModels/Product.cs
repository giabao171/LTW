using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021010.DomainModels
{
    /// <summary>
    /// Mặt hàng
    /// </summary>
    public class Product
    {
        ///<summary>
        ///Mã mặt hàng
        ///</summary>
        public int ProductID { get; set; }
        ///<summary>
        ///Tên mặt hàng
        ///</summary>
        public string ProductName { get; set; }
        ///<summary>
        ///Mã nhà cung cấp
        ///</summary>
        public int SupplierID { get; set; }
        ///<summary>
        ///Mã loại hàng
        ///</summary>
        public int CategoryID { get; set; }
        ///<summary>
        ///Đơn vị tính
        ///</summary>
        public string Unit { get; set; }
        ///<summary>
        ///Giá mặt hàng
        ///</summary>
        public decimal Price { get; set; }
        ///<summary>
        ///Đường dẫn mặt hàng
        ///</summary>
        public string Photo { get; set; }
    }
}
