using _19T1021010.DomainModels;
using _19T1021201.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021010.Web.Models
{
    /// <summary>
    /// Kết quả tìm kiếm dưới dạng phân trang
    /// </summary>
    public class ShipperSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// Danh sách người vận chuyển
        /// </summary>
        public List<Shipper> Data { get; set; }
    }
}