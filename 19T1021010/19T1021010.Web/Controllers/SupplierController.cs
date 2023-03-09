using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021010.DomainModels;
using _19T1021010.BusinessLayers;
using _19T1021010.Web.Models;

namespace _19T1021010.Web.Controllers
{
    [Authorize]
    public class SupplierController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string SUPPLIER_SEARCH = "SearchSupplierCondition";

        /// <summary>
        /// Giao diện hiển thị nhà cung cấp
        /// </summary>
        /// <returns></returns>
        //public ActionResult Index(int page = 1, int pageSize = 5, string searchValue ="")
        //{
        //    int rowCount = 0;
        //    var model = CommonDataService.ListOfSuppliers(page, pageSize, searchValue, out rowCount);

        //    int pageCount = rowCount / pageSize;
        //    if (rowCount % pageSize > 0)
        //        pageCount += 1;

        //    ViewBag.Page = page;
        //    ViewBag.PageCount = pageCount;
        //    ViewBag.RowCount = rowCount;
        //    ViewBag.PageSize = pageSize;
        //    ViewBag.SearchValue = searchValue;

        //    return View(model);
        //}


        public ActionResult Index()
        {

            PaginationSearchInput condition = Session[SUPPLIER_SEARCH] as PaginationSearchInput;
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
            var data = CommonDataService.ListOfSuppliers(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);

            var result = new SupplierSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data,
            };

            Session[SUPPLIER_SEARCH] = condition;
            return View(result);
        }

        /// <summary>
        /// Giao diện để bổ sung nhà cung cấp mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Supplier()
            {
                SupplierID = 0
            };

            ViewBag.title = "Bổ sung nhà cung cấp";
            return View("Edit", data);
        }

        /// <summary>
        /// Sửa nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }

            var data = CommonDataService.GetSupplier(id);

            if (data == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.title = "Cập nhật nhà cung cấp";
            return View(data);
        }

        /// <summary>
        /// Lưu thông tin thêm hoặc cập nhật nhà cung cấp
        /// </summary>
        /// <param name="data">Thông tin nhà cung cấp</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Supplier data)
        {
            try 
            {
                //Kiểm soát đầu vào
                if (string.IsNullOrWhiteSpace(data.SupplierName))
                    ModelState.AddModelError("SupplierName", "Tên không được để trống");
                if (string.IsNullOrWhiteSpace(data.ContactName))
                    ModelState.AddModelError("ContactName", "Tên giao dịch không được để trống");
                if (string.IsNullOrWhiteSpace(data.Phone))
                    ModelState.AddModelError("Phone", "Số điện tho không được để trống");
                if (string.IsNullOrWhiteSpace(data.Address))
                    ModelState.AddModelError("Address", "Địa chỉ không được để trống");
                if (string.IsNullOrWhiteSpace(data.City))
                    ModelState.AddModelError("City", "Thành phố không được để trống");
                if (string.IsNullOrWhiteSpace(data.Country))
                    ModelState.AddModelError("Country", "Vui lòng chọn quốc gia");
                if (string.IsNullOrWhiteSpace(data.PostalCode))
                    ModelState.AddModelError("PostalCode", "Mã bưu chính không được để trống");

                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.SupplierID != 0 ? "Cập nhật nhà cung cấp" : "Bổ sung nhà cung cấp";
                    return View("Edit", data);
                }

                if (data.SupplierID == 0)
                {
                    CommonDataService.AddSupplier(data);
                }
                else
                {
                    CommonDataService.UpdateSupplier(data);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // ghi lại log lỗi
                return Content("Có lỗi xảy ra, vui lòng thử lại sau :))");
            }
            
        }

        /// <summary>
        /// Xóa nhà cung cấp
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
                var data = CommonDataService.GetSupplier(id);
                if (data == null)
                {
                    return RedirectToAction("Index");
                }
                return View(data);

            }
            else
            {
                if(!CommonDataService.InSusedSupplier(id))
                    CommonDataService.DeleteSupplier(id);
                return RedirectToAction("Index");
            } 
            
        }

    }
}