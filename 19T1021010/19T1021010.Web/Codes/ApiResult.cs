using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021010.Web
{
    /// <summary>
    /// Kết quả trả về của API
    /// </summary>
    public class ApiResult
    {
        /// <summary>
        /// 1: Thành công, 0: lỗi
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// Thông báo lỗi (nếu có)
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Dữ liệu trả về (nếu có)
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Tạo ra mã thông báo thất bại
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ApiResult CreateFailResult(string msg)
        {
            return new ApiResult()
            {
                Code = 0,
                Message = msg,
                Data = null
            };
        }
        /// <summary>
        /// Tạo ra mã thông báo thành công
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ApiResult CreateSucessResult(object data = null)
        {
            return new ApiResult()
            {
                Code = 1,
                Message = "",
                Data = data
            };
        }
    }
}