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
    public class CategoryController : Controller
    {
        private const int PAGE_SIZE = 8;
        private const string CATEGORY_SEARCH = "SearchCategoryCondition";
        /// <summary>
        /// 
        /// Giao diện hiểm thị loại hàng
        /// </summary>
        /// <returns></returns>

        public ActionResult Index()
        {
            PaginationSearchInput condition = Session[CATEGORY_SEARCH] as PaginationSearchInput;

            if(condition == null)
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
            var data = CommonDataService.ListOfCategories(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);

            var result = new CategorySearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };

            Session[CATEGORY_SEARCH] = condition;
            return View(result);
        }

        /// <summary>
        /// Giao diện để bổ sung loại hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Category()
            {
                CategoryID = 0
            };

            ViewBag.title = "Bổ sung loại hàng";
            return View("Edit", data);
        }

        /// <summary>
        /// Sửa một nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }

            var data = CommonDataService.GetCategory(id);

            if (data == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Title = "Cập nhật loại hàng";
            return View(data);
        }

        /// <summary>
        /// Lưu thông tin thêm hoặc cập nhật loại hàng
        /// </summary>
        /// <param name="data">Thông tin loại hàng</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Category data)
        {
            try
            {
                //Kiểm soát đầu vào
                if (string.IsNullOrWhiteSpace(data.CategoryName))
                    ModelState.AddModelError("CategoryName", "Tên loại hàng không được để trống");
                if (string.IsNullOrWhiteSpace(data.Description))
                    ModelState.AddModelError("Description", "Tên loại hàng không được để trống");

                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.CategoryID == 0 ? "Bổ sung loại hàng" : "Cập nhật loại hàng";
                    return View("Edit", data);
                }
                if (data.CategoryID == 0)
                {
                    CommonDataService.AddCategory(data);
                }
                else
                {
                    CommonDataService.UpdateCategory(data);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content("Có lỗi xãy ra. Vui lòng thử lại sau!");
            }

            
        }

        /// <summary>
        /// Xóa một nhà cung cấp
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
                var data = CommonDataService.GetCategory(id);
                if (data == null)
                {
                    return RedirectToAction("Index");
                }
                return View(data);
            }
            else
            {
                if(!CommonDataService.InSusedCategory(id))
                    CommonDataService.DeleteCategory(id);
                return RedirectToAction("Index");
            }
        }

    }

}