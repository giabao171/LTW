using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021010.DataLayers;
using _19T1021010.DomainModels;

namespace _19T1021010.BusinessLayers
{
    /// <summary>
    /// Các chức năng liên quan đến tài khoẳn
    /// </summary>
    public static class UserAccountService
    {
        private static IUserAccountDAL employeeAccountDB;
        private static IUserAccountDAL customerAccountDB;
        /// <summary>
        /// 
        /// </summary>
        static UserAccountService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            employeeAccountDB = new DataLayers.SQLServer.EmployeeAccountDAL(connectionString);
            customerAccountDB = new DataLayers.SQLServer.CustomerAccountDAL(connectionString);


        }

        public static UserAccount Authorize(AccountTypes accountType, string userName, string password)
        {
            if(accountType == AccountTypes.Employee)
            {
                return employeeAccountDB.Authorize(userName, password);
            }    
            else
            {
                return customerAccountDB.Authorize(userName, password);
            }
        }

        public static bool ChangePassword(AccountTypes accountType, string userName, string oldPassword, string newPassword)
        {
            if (accountType == AccountTypes.Employee)
            {
                return employeeAccountDB.ChangePassword(userName, oldPassword, newPassword);
            }
            else
            {
                return customerAccountDB.ChangePassword(userName, oldPassword, newPassword);
            }
        }
    }
}
