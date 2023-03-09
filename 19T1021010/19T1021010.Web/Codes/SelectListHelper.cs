using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021010.BusinessLayers;
using _19T1021010.DomainModels;

namespace _19T1021010.Web
{
    //Cung cấp một số hàm tiện ích liên quan đến SelectList
    public static class SelectListHelper
    {
        public static List<SelectListItem> Countries()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "-- Chọn quốc gia --"
            });
            foreach(var item in CommonDataService.ListOfCountries())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CountryName,
                    Text = item.CountryName
                });
            }

            
            return list;
        }

        public static List<SelectListItem> Categories()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "-- Chọn loại hàng --"
            });
            foreach (var item in CommonDataService.ListOfCategories(""))
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CategoryID.ToString(),
                    Text = item.CategoryName
                });
            }


            return list;
        }

        public static List<SelectListItem> Suppliers()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "-- Chọn nhà cung cấp --"
            });
            foreach (var item in CommonDataService.ListOfSuppliers(""))
            {
                list.Add(new SelectListItem()
                {
                    Value = item.SupplierID.ToString(),
                    Text = item.SupplierName
                });
            }


            return list;
        }

        public static List<SelectListItem> Customers()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "-- Chọn nhà khách hàng --"
            });
            foreach (var item in CommonDataService.ListOfCustomers(""))
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CustomerID.ToString(),
                    Text = item.CustomerName
                });
            }


            return list;
        }

        public static List<SelectListItem> Emplyees()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "-- Chọn nhà nhân viên --"
            });
            foreach (var item in CommonDataService.ListOfEmployees(""))
            {
                list.Add(new SelectListItem()
                {
                    Value = item.EmployeeID.ToString(),
                    Text = $"{item.FirstName} {item.LastName}"
                });
            }


            return list;
        }
    }
}