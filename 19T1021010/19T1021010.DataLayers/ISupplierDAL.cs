using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021010.DomainModels;

namespace _19T1021010.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý dữ liệu trên nhà cung cấp
    /// Sửa dụng cách này này làm việc code lặp đi lặp lại => không đúng
    /// => dùng cách viết Generate
    /// </summary>
    public interface ISupplierDAL
    {
        /// <summary>
        /// Lấy thông tin nhà cung cấp dựa vào mã nhà cung cấp
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        Supplier Get(int supplierID);

        /// <summary>
        /// Tìm kiếm và lấy danh sách các nhà cung cấp dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng hiển thị trên mỗi trang (0 tức là không yêu cầu phân trang)</param>
        /// <param name="searchValue">Tên cần tìm kiếm (chuối rỗng nếu không tìm kiếm theo tên)</param>
        /// <returns></returns>
        IList<Supplier> List(int page = 1, int pageSize = 0, string searchValue = "");

        /// <summary>
        /// Đếm soosnhaf cung cấp tìm được
        /// </summary>
        /// <param name="searchValue">Tên cần tìm kiếm (chuỗi rông nếu không tìm kiếm theo tên)</param>
        /// <returns></returns>
        int Count(string searchValue = "");

        /// <summary>
        /// Bổ sung một nhà cung cấp
        /// </summary>
        /// <param name="data">Đối tượng chứa nhà cung cấp</param>
        /// <returns>ID của nhà cung cấp được tạo mới</returns>
        int Add(Supplier data);

        /// <summary>
        /// Cập nhật thông tin nhà cung cấp
        /// </summary>
        /// <param name="data">Đối tượng chứa thông tin cập nhật nhà cung cấp</param>
        /// <returns></returns>
        bool Update(Supplier data);

        /// <summary>
        /// Xóa nhà cung cấp
        /// </summary>
        /// <param name="supplierID">Mã nhà cung cấp cần xóa</param>
        /// <returns></returns>
        bool Dalete(int supplierID);

        /// <summary>
        /// Kiểm tả nhà cung cấp có dữu liệu liên quan hay không
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        bool InUsed(int supplierID);
    }
}
