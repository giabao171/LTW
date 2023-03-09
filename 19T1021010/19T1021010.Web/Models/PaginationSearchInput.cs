using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021010.Web.Models
{
    /// <summary>
    /// Biểu diễn dữ liệu đầu vào để tìm kiếm, phân trang chung
    /// </summary>
    public class PaginationSearchInput
    {
        /// <summary>
        /// Trang cần hiển thị
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Số dòng hiển thị trên mỗi trang
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Giá trị cần tìm
        /// </summary>
        public string SearchValue { get; set; }

  
    }
    public class BaseSearchInputProduct : PaginationSearchInput
    {
        public int SupplierID { get; set; } = 0;
        public int CategoryID { get; set; } = 0;
    }

    public class BaseSearchInputOder : PaginationSearchInput
    {
        public int Status { get; set; } = 1;
        
    }
}