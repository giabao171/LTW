using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021010.DataLayers
{
    //DDingj nghĩa các phép xử lý chung cho các dữ liệu đơn giản trên các bảng
    public interface ICommonDAL<T> where T : class
    {
        /// <summary>
        /// Lấy thông tin nhà cung cấp dựa vào mã nhà cung cấp
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);

        /// <summary>
        /// Tìm kiếm và lấy danh sách các dữ liệu dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng hiển thị trên mỗi trang (0 tức là không yêu cầu phân trang)</param>
        /// <param name="searchValue">Tên cần tìm kiếm (chuối rỗng nếu không tìm kiếm theo tên)</param>
        /// <returns></returns>
        IList<T> List(int page = 1, int pageSize = 0, string searchValue = "");

        /// <summary>
        /// Đếm soosnhaf cung cấp tìm được
        /// </summary>
        /// <param name="searchValue">Tên cần tìm kiếm (chuỗi rông nếu không tìm kiếm theo tên)</param>
        /// <returns></returns>
        int Count(string searchValue = "");

        /// <summary>
        /// Bổ sung dữ liệu
        /// </summary>
        /// <param name="data">Đối tượng chứa dữ liệu</param>
        /// <returns>ID của dữ liệu được tạo mới</returns>
        int Add(T data);

        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="data">Đối tượng chứa thông tin cập nhật dữ liệu</param>
        /// <returns></returns>
        bool Update(T data);

        /// <summary>
        /// Xóa dữu liệu
        /// </summary>
        /// <param name="id">id dữ liệu cần xóa</param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// Kiểm trả dữu liệu có dữ liệu liên quan hay không
        /// </summary>
        /// <param name="id">id dữ liệu</param>
        /// <returns></returns>
        bool InUsed(int id);
    }
}
