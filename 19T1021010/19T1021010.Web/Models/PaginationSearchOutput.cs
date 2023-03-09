using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021201.Web.Models
{
    /// <summary>
    /// Lớp cơ sở cho các lớp lưu trữ kết quả tìm kiếm dưới dạng phân trang
    /// </summary>
    public abstract class PaginationSearchOutput
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
        /// giá trị cần tìm kiếm
        /// </summary>
        public string SearchValue { get; set; }

        /// <summary>
        /// Số dòng tìm được
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int PageCount
        {
            get
            {
                if (PageSize == 0)
                    return 1;

                int p = RowCount / PageSize;
                if (RowCount % PageSize > 0)
                    p += 1;
                return p;

            }
        }
    }

    public class BasePaginationResultProduct : PaginationSearchOutput
    {
        public int SupplierID { get; set; }

        public int CategoryID { get; set; }
    }

    public class BasePaginationResultOrder : PaginationSearchOutput
    {
        public int Status { get; set; } = 1;
    }
}