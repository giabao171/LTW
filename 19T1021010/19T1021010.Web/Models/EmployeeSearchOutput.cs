using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021010.DomainModels;
using _19T1021201.Web.Models;

namespace _19T1021010.Web.Models
{
    /// <summary>
    /// Kết quả tìm kiếm phân trang
    /// </summary>
    public class EmployeeSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// Danh sách nhân viên
        /// </summary>
        public List<Employee> Data { get; set; }
    }
}