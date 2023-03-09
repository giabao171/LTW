using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021010.DomainModels
{
    /// <summary>
    /// Thông tin người vận chuyển
    /// </summary>
    public class Shipper
    {
        /// <summary>
        /// Mã người vận chuyển
        /// </summary>
        public int ShipperID { get; set; }

        /// <summary>
        /// Tên người vận chuyển
        /// </summary>
        public string ShipperName { get; set; }

        /// <summary>
        /// Số điện thoại người vận chuyển
        /// </summary>
        public string Phone { get; set; }
    }
}
