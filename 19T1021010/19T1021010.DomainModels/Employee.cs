using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021010.DomainModels
{
    /// <summary>
    /// Thông tin nhân viên
    /// </summary>
    public class Employee
    {
        public DateTime myDateTime;

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// Tên họ (đếm)
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Tên 
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Ảnh đại diện
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Mật khẩu đăng nhập
        /// </summary>
        public string Password { get; set; }
    }
}
