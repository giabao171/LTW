using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021010.DomainModels;
using _19T1021010.BusinessLayers;
using _19T1021010.Web.Models;
using _19T1021010.Web;

namespace _19T1021010.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private const int PAGE_SIZE = 8;
        private const string EMPLOYEE_SEARCH = "SearchEmployeeCondition";

        /// <summary>
        /// Giao diện hiển thị nhân viên
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            PaginationSearchInput condition = Session[EMPLOYEE_SEARCH] as PaginationSearchInput;

            if (condition == null)
            {
                condition = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = "",
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
            var data = CommonDataService.ListOfEmployees(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);

            var result = new EmployeeSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session[EMPLOYEE_SEARCH] = condition;
            return View(result);
        }

        /// <summary>
        /// Giao diện bổ sung nhân viên
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {

            var data = new Employee()
            {
                EmployeeID = 0
            };

            ViewBag.Title = "Bổ sung nhân viên";
            return View("Edit", data);
        }

        /// <summary>
        /// Sưa nhân viên
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            var data = CommonDataService.GetEmployee(id);
            if (data == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Title = "Cập nhật nhân viên";
            return View(data);
        }

        /// <summary>
        /// Lưu thông tin thêm hoặc cập nhật nhân viên
        /// </summary>
        /// <param name="data">Thông tin nhân viênp</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Employee data, string birthday, HttpPostedFileBase uploadPhoto)
        {
            try
            {
                DateTime? d = Converter.DMYStringToDateTime(birthday);
                if (d == null)
                {
                    ModelState.AddModelError("BirthDate", $"{birthday} sai định dạng ngày");
                }
                else
                {
                    DateTime startDay = new DateTime(900, 1, 1);
                    DateTime endDay = new DateTime(9999, 12, 31);
                    if (d > startDay && d < endDay)
                        data.BirthDate = d.Value;
                    else
                    {
                        ModelState.AddModelError("BirthDate", $"{birthday} sai định dạng ngày");
                    }
                }

                //Kiểm soát đầu vào
                if (string.IsNullOrWhiteSpace(data.LastName))
                    ModelState.AddModelError("LastName", "Tên nhân viên không được để trống");
                if (string.IsNullOrWhiteSpace(data.FirstName))
                    ModelState.AddModelError("FirstName", "Tên nhân viên không được để trống");
                 

                if (string.IsNullOrWhiteSpace(data.Email))
                    ModelState.AddModelError("Email", "Email không được để trống");
                if (string.IsNullOrWhiteSpace(data.Notes))
                    ModelState.AddModelError("Notes", "Ghi chú không được để trống");
                if (uploadPhoto == null)
                    ModelState.AddModelError("Photo", "Ảnh không được để trống");

                //if (string.IsNullOrWhiteSpace(data.Photo))
                //    ModelState.AddModelError("Photo", "Ảnh không được để trống");

                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.EmployeeID == 0 ? "Bổ sung nhân viên" : "Cập nhật nhân viên";
                    return View("Edit", data);
                }

                if (uploadPhoto != null)
                {
                    string path = Server.MapPath("~/Photo");
                    string fileName = $"{DateTime.Now.Ticks} {uploadPhoto.FileName}";
                    string filePath = System.IO.Path.Combine(path, fileName);
                    uploadPhoto.SaveAs(filePath);
                    data.Photo = fileName;
                }
                
                if (data.EmployeeID == 0)
                {
                    CommonDataService.AddEmployee(data);
                }
                else
                {
                    CommonDataService.UpdateEmployee(data);
                }

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return Content("Có lỗi xãy ra. Vui lòng thử lại sau!");
            }
            
        }

        /// <summary>
        /// Xóa nhân viên
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
                var data = CommonDataService.GetEmployee(id);
                if (data == null)
                {
                    RedirectToAction("Index");
                }
                return View(data);
            }
            else
            {
                if(!CommonDataService.InSusedEmployee(id))
                    CommonDataService.DeleteEmployee(id);
                return RedirectToAction("Index");
            }
            
        }
    }
}