﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021010.DomainModels;
using System.Data.SqlClient;
using System.Data;


namespace _19T1021010.DataLayers.SQLServer
{
    /// <summary>
    /// Cài đặt phép xử lý dữ liệu cho người vận chuyển
    /// </summary>
    public class ShipperDAL : _BaseDAL, ICommonDAL<Shipper>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public ShipperDAL(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// Thêm một người vận chuyển
        /// </summary>
        /// <param name="data">Thông tin của người vận chuyển cần thêm</param>
        /// <returns></returns>
        public int Add(Shipper data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Shippers(ShipperName, Phone)
                                    VALUES(@ShipperName, @Phone);
                                    SELECT SCOPE_IDENTITY()";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ShipperName", data.ShipperName);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);

                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// Đếm số lượng người vận chuyển tìm kiếm được theo tên
        /// </summary>
        /// <param name="searchValue">Giá trị tìm kiếm theo tên</param>
        /// <returns></returns>
        public int Count(string searchValue = "")
        {
            int count = 0;

            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT	COUNT(*)
                                    FROM	Shippers 
                                    WHERE	(@SearchValue = N'')
	                                    OR	(
			                                    (ShipperName LIKE @SearchValue)
		                                    )";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@SearchValue", searchValue);

                count = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return count;
        }

        /// <summary>
        /// Xóa một người vận chuyển
        /// </summary>
        /// <param name="id">Id của người vận chuyển</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Shippers 
                                    WHERE ShipperID = @ShipperID AND NOT EXISTS(SELECT * FROM Orders WHERE ShipperID = @ShipperID)";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ShipperID", id);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// Lấy thông tin của một người vận chuyển
        /// </summary>
        /// <param name="id">Id của người vận chuyển</param>
        /// <returns></returns>
        public Shipper Get(int id)
        {
            Shipper data = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM  Shippers  WHERE ShipperID = @ShipperID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ShipperID", id);
                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    data = new Shipper()
                    {
                        ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                        ShipperName = Convert.ToString(dbReader["ShipperName"]),
                        Phone = Convert.ToString(dbReader["Phone"]),
                        
                    };
                }
                cn.Close();
            }
            return data;
        }

        /// <summary>
        /// Kiểm tra dữ liệu của người vận chuyển có tồn tại trong bảng khác không
        /// </summary>
        /// <param name="id">Id của người vận chuyển</param>
        /// <returns></returns>
        public bool InUsed(int id)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT CASE 
                                                WHEN EXISTS(SELECT * FROM Orders WHERE ShipperID = @ShipperID) THEN 1 
                                                ELSE 0 
                                            END";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ShipperID", id);

                result = Convert.ToBoolean(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// Hiển thị thông tin các người vận chuyển dưới dạng phân trang
        /// </summary>
        /// <param name="page">Số trang</param>
        /// <param name="pageSize">Số lượng lượng người vận chuyển trên 1 trang</param>
        /// <param name="searchValue">Tên tìm kiếm(nếu không nhập thì lấy toàn bộ)</param>
        /// <returns></returns>
        public IList<Shipper> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<Shipper> data = new List<Shipper>();

            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT *
                                    FROM 
                                    (
	                                    SELECT	*, ROW_NUMBER() OVER (ORDER BY ShipperName) AS RowNumber
	                                    FROM	Shippers 
	                                    WHERE	(@SearchValue = N'')
		                                    OR	(
				                                    (ShipperName LIKE @SearchValue)

			                                    )
                                    ) AS t
                                    WHERE (@PageSize = 0) OR (t.RowNumber BETWEEN (@Page - 1) * @PageSize + 1 AND @Page * @PageSize);";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@Page", page);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);
                cmd.Parameters.AddWithValue("@SearchValue", searchValue);

                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dbReader.Read())
                {
                    data.Add(new Shipper()
                    {
                        ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                        ShipperName = Convert.ToString(dbReader["ShipperName"]),
                        Phone = Convert.ToString(dbReader["Phone"]),                       
                    });
                }
                dbReader.Close();
                cn.Close();
            }

            return data;
        }

        /// <summary>
        /// Câp nhật thông tin của một người vận chuyển
        /// </summary>
        /// <param name="data">Id của người vận chuyển</param>
        /// <returns></returns>
        public bool Update(Shipper data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Shippers
                                    SET ShipperName = @ShipperName, Phone = @Phone
                                    WHERE ShipperID = @ShipperID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ShipperID", data.ShipperID);
                cmd.Parameters.AddWithValue("@ShipperName", data.ShipperName);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
    }
}
