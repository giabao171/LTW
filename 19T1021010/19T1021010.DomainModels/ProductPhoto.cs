using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021010.DomainModels
{
    /// <summary>
    /// Ảnh của mặt hàng
    /// </summary>
    public class ProductPhoto
    {
        ///<summary>
        ///Mã ảnh mặt hàng
        ///</summary>
        public long PhotoID { get; set; }
        ///<summary>
        ///Mã mặt hàng
        ///</summary>
        public int ProductID { get; set; }
        ///<summary>
        ///Đường dẫn ảnh
        ///</summary>
        public string Photo { get; set; }
        ///<summary>
        ///Mô tả ảnh
        ///</summary>
        public string Description { get; set; }
        ///<summary>
        ///Thứ tự hiển thị của ảnh
        ///</summary>
        public int DisplayOrder { get; set; }
        ///<summary>
        ///Nếu muốn ảnh ẩn hay hiển thị ảnh
        ///</summary>
        public bool IsHidden { get; set; }
    }
}
