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
    public class ShipperController : Controller
    {
        private const int PAGE_SIZE = 8;
        private const string SHIPPER_SEARCH = "SearchShipperCondition";
        /// <summary>
        /// Giao diện hiển thị người vận chuyển
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            PaginationSearchInput condition = Session[SHIPPER_SEARCH] as PaginationSearchInput;
            if( condition == null)
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

        public ActionResult Search(PaginationSearchInput condition)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfShippers(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);

            var result = new ShipperSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };

            Session[SHIPPER_SEARCH] = condition;
            return View(result);
        }

        /// <summary>
        /// Giao diện bổ sung nhân viên giao hầng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Shipper()
            {
                ShipperID = 0
            };

            ViewBag.title = "Bổ sung người vận chuyển";
            return View("Edit", data);
        }

        /// <summary>
        /// Sửa nhân viên giao hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            var data = CommonDataService.GetShipper(id);
            if (data == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Title = "Cập nhật nhân viên giao hàng";
            return View(data);
        }

        /// <summary>
        /// Lưu thông tin thêm hoặc cập nhật người vận chuyển
        /// </summary>
        /// <param name="data">Thông tin người vận chuyển</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Shipper data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.ShipperName))
                    ModelState.AddModelError("ShipperName", "Tên người giao hàng không được để trống");


                if (string.IsNullOrWhiteSpace(data.Phone))
                    ModelState.AddModelError("Phone", "Số điện thoại không được để trống");

                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.ShipperID == 0 ? "Bổ sung người giao hàng" : "Cập nhật người giao hàng";
                    return View("Edit", data);
                }

                if (data.ShipperID == 0)
                {
                    CommonDataService.AddShipper(data);
                }
                else
                {
                    CommonDataService.UpdateShipper(data);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            { 
                return Content("Có lỗi xãy ra. Vui lòng thử lại sau!"); 
            }
        }

        /// <summary>
        /// Xóa nhân viên giao hàng
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
                var data = CommonDataService.GetShipper(id);
                if (data == null)
                {
                    RedirectToAction("Index");
                }
                return View(data);
            }
            else
            {
                if(!CommonDataService.InSusedShipper(id))
                    CommonDataService.Deleteshipper(id);
                return RedirectToAction("Index");
            }

        }
    }
}