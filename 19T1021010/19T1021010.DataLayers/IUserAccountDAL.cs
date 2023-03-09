using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021010.DomainModels;

namespace _19T1021010.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý liên quan đến dữ liệu người dùng
    /// </summary>
    public interface IUserAccountDAL
    {
        /// <summary>
        /// Kiểm tra tê đăng nhập và mật khẩu có hượp lệ không
        /// Nếu hượp lệ trả về thông tin tài khoản, ngược lại trả về null
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserAccount Authorize(string userName, string password);

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }
}
