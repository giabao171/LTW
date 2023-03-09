using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021010.DataLayers;
using _19T1021010.DomainModels;
using System.Configuration;

namespace _19T1021010.BusinessLayers
{
    /// <summary>
    /// Các chức năng nghiệp vụ liên quan đến: nhà cung cấp, khách hàng, người giao hàng,  nhân viên, loại hàng
    /// </summary>
    public static class CommonDataService
    {

        private static ICountryDAL countryDB;
        private static ICommonDAL<Supplier> supplierDB;
        private static ICommonDAL<Shipper> shipperDB;
        private static ICommonDAL<Category> categoryDB;
        private static ICommonDAL<Customer> customerDB;
        private static ICommonDAL<Employee> employeeDB;
        private static ICommonDAL<Product> productDB;

        /// <summary>
        /// Ctor
        /// </summary>
        static CommonDataService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            countryDB = new DataLayers.SQLServer.CountryDAL(connectionString);
            supplierDB = new DataLayers.SQLServer.SupplierDAL(connectionString);
            shipperDB = new DataLayers.SQLServer.ShipperDAL(connectionString);
            categoryDB = new DataLayers.SQLServer.CategoryDAL(connectionString);
            customerDB = new DataLayers.SQLServer.CustomerDAL(connectionString);
            employeeDB = new DataLayers.SQLServer.EmployeeDAL(connectionString);
        }

        #region Các nghiệp vụ liên quan đến quốc gia
        /// <summary>
        /// Lấy danh sách các quốc gia
        /// </summary>
        /// <returns></returns>
        public static List<Country> ListOfCountries()
        {
            return countryDB.List().ToList();
        }
        #endregion

        #region Các nghiệp vụ liên quan đến nhà cung cấp
        /// <summary>
        /// Tìm kiếm, lấy danh sách các nhà cung câp dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng trên mỗi trang(nếu không phân trang thì bằng 0)</param>
        /// <param name="searchValue">Giá trị tìm kiếm(chuỗi rỗng thì khoong tìm kiếm)</param>
        /// <param name="rowcount">Output: Tổng số dòng tìm được</param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers(int page, int pageSize, string searchValue, out int rowcount)
        {
            rowcount = supplierDB.Count(searchValue);
            return supplierDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// Tìm kiếm, lấy danh sách các nhà cung câp dưới dạng không phân trang
        /// </summary>
        /// <param name="searchValue">Giá trị tìm kiếm(chuỗi rỗng thì khoong tìm kiếm)</param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers(string searchValue)
        {
            return supplierDB.List(1, 0, searchValue).ToList();
        }

        /// <summary>
        /// Bổ sung nhà cung cấp
        /// </summary>
        /// <param name="data">Thông tin nhà cung cấp</param>
        /// <returns>Mã nahf cung cấp được bổ sung</returns>
        public static int AddSupplier(Supplier data)
        {
            return supplierDB.Add(data);
        }

        /// <summary>
        /// Cập nhật thoogn tin một nhà cung cấp
        /// </summary>
        /// <param name="data">Thông tin cập nhật nhà cung cấp</param>
        /// <returns>Trả về true hoặc false</returns>
        public static bool UpdateSupplier(Supplier data)
        {
            return supplierDB.Update(data);
        }

        /// <summary>
        /// Xóa một nahf cung cấp
        /// </summary>
        /// <param name="supplierID">Mã nhà cung cấp</param>
        /// <returns>Trả về true hoặc false</returns>
        public static bool DeleteSupplier(int supplierID)
        {
            return supplierDB.Delete(supplierID);
        }

        /// <summary>
        /// Lấy thông tin một nhà cung cấp
        /// </summary>
        /// <param name="supplierID">Mã nhà cung cấp</param>
        /// <returns>Trả về thông tin nhà cung cấp</returns>
        public static Supplier GetSupplier(int supplierID)
        {
            return supplierDB.Get(supplierID);
        }

        /// <summary>
        /// Kiểm tra dữ liệu của nhà cung cấp có tồn tại trong bảng khác không
        /// </summary>
        /// <param name="supplierID">Mã nhà cung cấp</param>
        /// <returns>Trả về true haowcj false</returns>
        public static bool InSusedSupplier(int supplierID)
        {
            return supplierDB.InUsed(supplierID);
        }

        #endregion

        #region Các nghiệp vụ liên quan đến loại hàng
        /// <summary>
        /// Tìm kiếm, lấy danh sách các loại hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng trên mỗi trang(nếu không phân trang thì bằng 0)</param>
        /// <param name="searchValue">Giá trị tìm kiếm(chuỗi rỗng thì khoong tìm kiếm)</param>
        /// <param name="rowcount">Output: Tổng số dòng tìm được</param>
        /// <returns></returns>
        public static List<Category> ListOfCategories(int page, int pageSize, string searchValue, out int rowcount)
        {
            rowcount = categoryDB.Count(searchValue);
            return categoryDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// Tìm kiếm, lấy danh sách các loại hàng dưới dạng không phân trang
        /// </summary>
        /// <param name="searchValue">Giá trị tìm kiếm(chuỗi rỗng thì khoong tìm kiếm)</param>
        /// <returns></returns>
        public static List<Category> ListOfCategories(string searchValue)
        {
            return categoryDB.List(1, 0, searchValue).ToList();
        }

        /// <summary>
        /// Bổ sung loại hàng
        /// </summary>
        /// <param name="data">Thông tin loại hàng</param>
        /// <returns>Mã nahf cung cấp được bổ sung</returns>
        public static int AddCategory(Category data)
        {
            return categoryDB.Add(data);
        }

        /// <summary>
        /// Cập nhật thoogn tin một loại hàng
        /// </summary>
        /// <param name="data">Thông tin cập nhật loại hàng</param>
        /// <returns>Trả về true hoặc false</returns>
        public static bool UpdateCategory(Category data)
        {
            return categoryDB.Update(data);
        }

        /// <summary>
        /// Xóa một loại hàng
        /// </summary>
        /// <param name="CategoryID">Mã loại hàng</param>
        /// <returns>Trả về true hoặc false</returns>
        public static bool DeleteCategory(int CategoryID)
        {
            return categoryDB.Delete(CategoryID);
        }

        /// <summary>
        /// Lấy thông tin một loại hàng
        /// </summary>
        /// <param name="categoryID">Mã loại hnagf</param>
        /// <returns>Trả về thông tin loại hàng</returns>
        public static Category GetCategory(int categoryID)
        {
            return categoryDB.Get(categoryID);
        }

        /// <summary>
        /// Kiểm tra dữ liệu của loại hàng có tồn tại trong bảng khác không
        /// </summary>
        /// <param name="categoryID">Mã loại hàng</param>
        /// <returns>Trả về true haowcj false</returns>
        public static bool InSusedCategory(int categoryID)
        {
            return categoryDB.InUsed(categoryID);
        }

        #endregion

        #region Các nghiệp vụ liên quan đến khách hàng
        /// <summary>
        /// Tìm kiếm, lấy danh sách các khách hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng trên mỗi trang(nếu không phân trang thì bằng 0)</param>
        /// <param name="searchValue">Giá trị tìm kiếm(chuỗi rỗng thì khoong tìm kiếm)</param>
        /// <param name="rowcount">Output: Tổng số dòng tìm được</param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(int page, int pageSize, string searchValue, out int rowcount)
        {
            rowcount = customerDB.Count(searchValue);
            return customerDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// Tìm kiếm, lấy danh sách các khách hàng dưới dạng không phân trang
        /// </summary>
        /// <param name="searchValue">Giá trị tìm kiếm(chuỗi rỗng thì khoong tìm kiếm)</param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(string searchValue)
        {
            return customerDB.List(1, 0, searchValue).ToList();
        }

        /// <summary>
        /// Bổ sung khách hàng
        /// </summary>
        /// <param name="data">Thông tin khách hàng</param>
        /// <returns>Mã khách hàng được bổ sung</returns>
        public static int AddCustomer(Customer data)
        {
            return customerDB.Add(data);
        }

        /// <summary>
        /// Cập nhật thông tin một khách hàng
        /// </summary>
        /// <param name="data">Thông tin cập nhật khách hàng</param>
        /// <returns>Trả về true hoặc false</returns>
        public static bool UpdateCustomer(Customer data)
        {
            return customerDB.Update(data);
        }

        /// <summary>
        /// Xóa một khách hàng
        /// </summary>
        /// <param name="customerID">Mã khách hàng</param>
        /// <returns>Trả về true hoặc false</returns>
        public static bool DeleteCustomer(int customerID)
        {
            return customerDB.Delete(customerID);
        }

        /// <summary>
        /// Lấy thông tin một khách hàng
        /// </summary>
        /// <param name="customerID">Mã khách hàngp</param>
        /// <returns>Trả về thông tin khách hàng</returns>
        public static Customer GetCustomer(int customerID)
        {
            return customerDB.Get(customerID);
        }

        /// <summary>
        /// Kiểm tra dữ liệu của khách hàng có tồn tại trong bảng khác không
        /// </summary>
        /// <param name="customerID">Mã khách hàng</param>
        /// <returns>Trả về true hoặc false</returns>
        public static bool InSusedCustomer(int customerID)
        {
            return customerDB.InUsed(customerID);
        }

        #endregion

        #region Các nghiệp vụ liên quan đến nhân viên
        /// <summary>
        /// Tìm kiếm, lấy danh sách các nhân viên dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng trên mỗi trang(nếu không phân trang thì bằng 0)</param>
        /// <param name="searchValue">Giá trị tìm kiếm(chuỗi rỗng thì khoong tìm kiếm)</param>
        /// <param name="rowcount">Output: Tổng số dòng tìm được</param>
        /// <returns></returns>
        public static List<Employee> ListOfEmployees(int page, int pageSize, string searchValue, out int rowcount)
        {
            rowcount = employeeDB.Count(searchValue);
            return employeeDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// Tìm kiếm, lấy danh sách các nhân viên dưới dạng không phân trang
        /// </summary>
        /// <param name="searchValue">Giá trị tìm kiếm(chuỗi rỗng thì khoong tìm kiếm)</param>
        /// <returns></returns>
        public static List<Employee> ListOfEmployees(string searchValue)
        {
            return employeeDB.List(1, 0, searchValue).ToList();
        }

        /// <summary>
        /// Bổ sung nhân viên
        /// </summary>
        /// <param name="data">Thông tin nhâ viên</param>
        /// <returns>Mã nhân viên được bổ sung</returns>
        public static int AddEmployee(Employee data)
        {
            return employeeDB.Add(data);
        }

        /// <summary>
        /// Cập nhật thông tin một nhân viên
        /// </summary>
        /// <param name="data">Thông tin cập nhật nhân viên</param>
        /// <returns>Trả về true hoặc false</returns>
        public static bool UpdateEmployee(Employee data)
        {
            return employeeDB.Update(data);
        }

        /// <summary>
        /// Xóa một nhân viên
        /// </summary>
        /// <param name="employeeID">Mã nhân viên</param>
        /// <returns>Trả về true hoặc false</returns>
        public static bool DeleteEmployee(int employeeID)
        {
            return employeeDB.Delete(employeeID);
        }

        /// <summary>
        /// Lấy thông tin một nhân viên
        /// </summary>
        /// <param name="EmployeeID">Mã nhân viênp</param>
        /// <returns>Trả về thông tin nhân viên</returns>
        public static Employee GetEmployee(int EmployeeID)
        {
            return employeeDB.Get(EmployeeID);
        }

        /// <summary>
        /// Kiểm tra dữ liệu của nhân viên có tồn tại trong bảng khác không
        /// </summary>
        /// <param name="EmployeeId">Mã nhân viên</param>
        /// <returns>Trả về true hoặc false</returns>
        public static bool InSusedEmployee(int EmployeeId)
        {
            return employeeDB.InUsed(EmployeeId);
        }

        #endregion

        #region Các nghiệp vụ liên quan đến người vận chuyển
        /// <summary>
        /// Tìm kiếm, lấy danh sách các người vận chuyển dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng trên mỗi trang(nếu không phân trang thì bằng 0)</param>
        /// <param name="searchValue">Giá trị tìm kiếm(chuỗi rỗng thì khoong tìm kiếm)</param>
        /// <param name="rowcount">Output: Tổng số dòng tìm được</param>
        /// <returns></returns>
        public static List<Shipper> ListOfShippers(int page, int pageSize, string searchValue, out int rowcount)
        {
            rowcount = shipperDB.Count(searchValue);
            return shipperDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// Tìm kiếm, lấy danh sách các người vận chuyển dưới dạng không phân trang
        /// </summary>
        /// <param name="searchValue">Giá trị tìm kiếm(chuỗi rỗng thì khoong tìm kiếm)</param>
        /// <returns></returns>
        public static List<Shipper> ListOfShippers(string searchValue)
        {
            return shipperDB.List(1, 0, searchValue).ToList();
        }

        /// <summary>
        /// Bổ sung nhà người vận chuyển
        /// </summary>
        /// <param name="data">Thông tin người vận chuyển</param>
        /// <returns>Mã nahf cung cấp được bổ sung</returns>
        public static int AddShipper(Shipper data)
        {
            return shipperDB.Add(data);
        }

        /// <summary>
        /// Cập nhật thông tin một người vận chuyển
        /// </summary>
        /// <param name="data">Thông tin cập nhật người vận chuyển</param>
        /// <returns>Trả về true hoặc false</returns>
        public static bool UpdateShipper(Shipper data)
        {
            return shipperDB.Update(data);
        }

        /// <summary>
        /// Xóa một người vận chuyển
        /// </summary>
        /// <param name="shipperID">Mã người vận chuyển</param>
        /// <returns>Trả về true hoặc false</returns>
        public static bool Deleteshipper(int shipperID)
        {
            return shipperDB.Delete(shipperID);
        }

        /// <summary>
        /// Lấy thông tin một nhà cung cấp
        /// </summary>
        /// <param name="shipperID">Mã nhà cung cấp</param>
        /// <returns>Trả về thông tin nhà cung cấp</returns>
        public static Shipper GetShipper(int shipperID)
        {
            return shipperDB.Get(shipperID);
        }

        /// <summary>
        /// Kiểm tra dữ liệu của người vận chuyển có tồn tại trong bảng khác không
        /// </summary>
        /// <param name="shipperID">Mã nhà cung cấp</param>
        /// <returns>Trả về true haowcj false</returns>
        public static bool InSusedShipper(int shipperID)
        {
            return shipperDB.InUsed(shipperID);
        }

        #endregion
    }
}
       

