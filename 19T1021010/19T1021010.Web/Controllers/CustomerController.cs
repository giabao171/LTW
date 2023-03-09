using _19T1021010.BusinessLayers;
using _19T1021010.DomainModels;
using _19T1021010.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021201.Web.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private const int PAGE_SIZE = 8;
        private const string CUSTOMER_SEARCH = "SearchCustomerCondition";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Customer
        public ActionResult Index()
        {
            PaginationSearchInput condition = Session[CUSTOMER_SEARCH] as PaginationSearchInput;

            if (condition == null)
            {
                condition = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = ""
                };
            }

            return View(condition);
        }

        /// <summary>
        /// Kết quả tìm kiếm
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Search(PaginationSearchInput condition)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfCustomers(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);
            var result = new CustomerSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session[CUSTOMER_SEARCH] = condition;

            return View(result);
        }

        /// <summary>
        /// Giao diện để bổ sung khách hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Customer()
            {
                CustomerID = 0
            };
            ViewBag.Title = "Bổ sung khách hàng";
            return View("Edit", data);
        }

        /// <summary>
        /// Sửa khách hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            if (id == 0 )
            {
                return RedirectToAction("Index");
            }

            var data = CommonDataService.GetCustomer(id);
            
            if (data == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Title = "Cập nhật khách hàng";
            return View(data);
        }

        /// <summary>
        /// Lưu thông tin khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer data)
        {
            try
            {
                //Kiểm soát lỗi
                if (string.IsNullOrWhiteSpace(data.CustomerName))
                    ModelState.AddModelError("CustomerName", "Tên khách hàng không được để trống");
                if (string.IsNullOrWhiteSpace(data.ContactName))
                    ModelState.AddModelError("ContactName", "Tên giao dịch không được để trống");
                if (string.IsNullOrWhiteSpace(data.Email))
                    ModelState.AddModelError("Email", "Email không được để trống");
                if (string.IsNullOrWhiteSpace(data.Address))
                    ModelState.AddModelError("Address", "Địa chỉ không được để trống");
                if (string.IsNullOrWhiteSpace(data.City))
                    ModelState.AddModelError("City", "Thành phố không được để trống");
                if (string.IsNullOrWhiteSpace(data.Country))
                    ModelState.AddModelError("Country", "Vui lòng chọn quốc gia");
                if (string.IsNullOrWhiteSpace(data.PostalCode))
                    ModelState.AddModelError("PostalCode", "Vui lòng chọn quốc gia");

                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.CustomerID != 0 ? "Cập nhật khách hàng" : "Bổ sung khách hàng";
                    return View("Edit", data);
                }

                if (data.CustomerID == 0)
                {
                    CommonDataService.AddCustomer(data);
                }
                else
                {
                    CommonDataService.UpdateCustomer(data);
                }
            }
            catch (Exception ex)
            {
                return Content("Có lỗi xảy ra, vui lòng thử lại sau :))");
            }
            

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Xóa khách hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }

            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetCustomer(id);
                if (data == null)
                {
                    return RedirectToAction("Index");
                }
                return View(data);
            }
            else
            {
                if(!CommonDataService.InSusedCustomer(id))
                    CommonDataService.DeleteCustomer(id);
                return RedirectToAction("Index");
            }
        }
    }
}