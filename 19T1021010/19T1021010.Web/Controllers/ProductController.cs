using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021010.Web.Models;
using _19T1021010.DomainModels;
using _19T1021010.BusinessLayers;

namespace _19T1021010.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("product")]
    public class ProductController : Controller
    {
        private const int PAGE_SIZE = 10;
        private const string PRODUCT_SEARCH = "SearchProductCondition";
        private const string PRODUCT_EDIT = "ProductEdit";
        /// <summary>
        /// Tìm kiếm, hiển thị mặt hàng dưới dạng phân trang
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            BaseSearchInputProduct condition = Session[PRODUCT_SEARCH] as BaseSearchInputProduct;
            if (condition == null)
            {
                condition = new BaseSearchInputProduct()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = "",
                    CategoryID = 0,
                    SupplierID = 0
                };
            }
            return View(condition);
        }

        /// <summary>
        /// Kết quả tìm kiếm
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Search(BaseSearchInputProduct condition)
        {
            int rowCount = 0;
            var data = ProductDataService.ListProducts(condition.Page, condition.PageSize, condition.SearchValue, condition.CategoryID, condition.SupplierID, out rowCount);
            var result = new ProductSearch()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                CategoryID = condition.CategoryID,
                SupplierID = condition.SupplierID,
                RowCount = rowCount,
                Data = data,
            };
            Session[PRODUCT_SEARCH] = condition;
            return View(result);
        }

        /// <summary>
        /// Tạo mặt hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Product()
            {
                //ProductID = 0
            };

            ViewBag.title = "Bổ sung mặt hàng";
            return View(data);
        }
        /// <summary>
        /// Cập nhật thông tin mặt hàng, 
        /// Hiển thị danh sách ảnh và thuộc tính của mặt hàng, điều hướng đến các chức năng
        /// quản lý ảnh và thuộc tính của mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }

            var data = ProductDataService.GetProduct(id);
            if (data == null)
            {
                return RedirectToAction("Index");
            }

            var dataProductPictures = ProductDataService.ListPhotos(id);
            var dataProductAttributes = ProductDataService.ListAttributes(id);
            ProductEditModel model = new ProductEditModel()
            {
                Product = data,
                Photos = dataProductPictures,
                Attributes = dataProductAttributes
            };
            
            ViewBag.title = "Cập nhật mặt hàng";
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveCreate(Product data, HttpPostedFileBase uploadPhoto)
        {
            try
            {
                //kiểm soát đầu vào
                if (string.IsNullOrWhiteSpace(data.ProductName))
                ModelState.AddModelError("ProductName", "Tên mặt hàng không được để trống");
                if (data.CategoryID == 0)
                    ModelState.AddModelError("CategoryID", "Loại hàng không được để trống");
                if (data.SupplierID == 0)
                    ModelState.AddModelError("SupplierID", "Nhà cung cấp không được để trống");
                if (string.IsNullOrWhiteSpace(data.Unit))
                    ModelState.AddModelError("Unit", "Đơn vị tính không được để trống");
                if (data.Price == 0 || data.Price < 0)
                    ModelState.AddModelError("Price", "Giá không hợp lệ");
            
                if (uploadPhoto != null)
                {
                    string path = Server.MapPath("~/Photo");
                    string fileName = $"{DateTime.Now.Ticks} {uploadPhoto.FileName}";
                    string filePath = System.IO.Path.Combine(path, fileName);
                    uploadPhoto.SaveAs(filePath);
                    data.Photo = $"/Photo/{fileName}";
                }
                if (string.IsNullOrWhiteSpace(data.Photo))
                    ModelState.AddModelError("Photo", "Ảnh không được để trống");

                if (!ModelState.IsValid)
                {
                    return View("Create", data);
                }

                var productNewId = ProductDataService.AddProduct(data);

                return RedirectToAction($"Edit/{productNewId}");
            }
            catch
            {
                // ghi lại log lỗi
                return Content("Có lỗi xảy ra, vui lòng thử lại sau :))");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEdit(ProductEditModel data, HttpPostedFileBase uploadPhoto)
        {
            try
            {
                //kiểm soát đầu vào
            if (string.IsNullOrWhiteSpace(data.Product.ProductName))
                ModelState.AddModelError("Product.ProductName", "Tên mặt hàng không được để trống");
            if (data.Product.CategoryID == 0)
                ModelState.AddModelError("Product.CategoryID", "Loại hàng không được để trống");
            if (data.Product.SupplierID == 0)
                ModelState.AddModelError("Product.SupplierID", "Nhà cung cấp không được để trống");
            if (string.IsNullOrWhiteSpace(data.Product.Unit))
                ModelState.AddModelError("Product.Unit", "Đơn vị tính không được để trống");
            if (string.IsNullOrWhiteSpace(data.Product.Price.ToString()))
                ModelState.AddModelError("Product.Price", "Giá không được để trống");

            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/Photo");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);
                data.Product.Photo = $"/Photo/{fileName}";
            }
            else
            {
                data.Product.Photo = ProductDataService.GetProduct(data.Product.ProductID).Photo;
            }

            if (!ModelState.IsValid)
            {
                var PhotosProduct = ProductDataService.ListPhotos(data.Product.ProductID);
                var AttributesProduct = ProductDataService.ListAttributes(data.Product.ProductID);
                var ProductData = ProductDataService.GetProduct(data.Product.ProductID);

                var model = new ProductEditModel()
                {
                    Product = ProductData,
                    Photos = PhotosProduct,
                    Attributes = AttributesProduct,
                };
                return View("Edit", model);
            }
            
            ProductDataService.UpdateProduct(data.Product);
            return RedirectToAction("Index");
            }
            catch
            {
                // ghi lại log lỗi
                return Content("Có lỗi xảy ra, vui lòng thử lại sau!");
            }
        }

        /// <summary>
        /// Xóa mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        public ActionResult Delete(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }

            if (Request.HttpMethod == "GET")
            {
                var data = ProductDataService.GetProduct(id);
                if (data == null)
                {
                    return RedirectToAction("Index");
                }
                return View(data);

            }
            else
            {
                
                if (ProductDataService.InUsedProduct(id) == false) 
                {
                    var listAttributes = ProductDataService.ListAttributes(id);
                    foreach (var item in listAttributes)
                        ProductDataService.DeleteAttribute(item.AttributeID);

                    var listPhotos = ProductDataService.ListPhotos(id);
                    foreach (var item in listPhotos)
                        ProductDataService.DeletePhoto(item.PhotoID);

                    ProductDataService.DeleteProduct(id);
                }

                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Các chức năng quản lý ảnh của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="photoID"></param>
        /// <returns></returns>
        [Route("photo/{method?}/{productID?}/{photoID?}")]
        public ActionResult Photo(string method = "add", int productID = 0, long photoID = 0)
        {
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung ảnh";
                    var PhotoModelCreate = new ProductPhoto()
                    {
                        PhotoID = 0,
                        ProductID = productID
                    };
                    return View(PhotoModelCreate);

                case "edit":
                    ViewBag.Title = "Thay đổi ảnh";
                    var photoModelEdit = ProductDataService.GetPhoto(photoID);
                    return View(photoModelEdit);

                case "delete":
                    ProductDataService.DeletePhoto(photoID);
                    return RedirectToAction($"Edit/{productID}"); //return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Lưu dữu liệu ảnh
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SavePhoto(ProductPhoto data, HttpPostedFileBase uploadPhoto)
        {
            try
            { 
                //Kiểm soát đầu vào
                if (string.IsNullOrWhiteSpace(data.Description))
                    ModelState.AddModelError("Description", "Mô tả không được để trống");
                if (data.DisplayOrder == 0 || data.DisplayOrder < 1)
                    ModelState.AddModelError("DisplayOrder", "Thứ tự hiển thị không hợp lệ");

                if (uploadPhoto != null)
                {
                    string path = Server.MapPath("~/Photo");
                    string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                    string filePath = System.IO.Path.Combine(path, fileName);
                    uploadPhoto.SaveAs(filePath);
                    data.Photo = $"/Photo/{fileName}";
                }
                if(string.IsNullOrWhiteSpace(data.Photo))
                    ModelState.AddModelError("Photo", "Ảnh không được để trống");

                if (data.IsHidden)
                    data.IsHidden = true;

                //return Content(data.IsHidden.ToString());



                if (!ModelState.IsValid)
                {
                    //ViewBag.Title = data.PhotoID != 0 ? "Cập nhật ảnh mô tả mặt hàng" : "Bổ sung ảnh mô tả mặt hàng";
                    return View("Photo", data);
                }

                if (data.PhotoID == 0)
                {
                    ProductDataService.AddPhoto(data);
                }
                else
                {
                    ProductDataService.UpdatePhoto(data);
                }
                return RedirectToAction($"Edit/{data.ProductID}");
            }
            catch
            {
                // ghi lại log lỗi
                return Content("Có lỗi xảy ra, vui lòng thử lại sau!");
            }
        }

        /// <summary>
        /// Các chức năng quản lý thuộc tính của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        [Route("attribute/{method?}/{productID}/{attributeID?}")]
        public ActionResult Attribute(string method = "add", int productID = 0, int attributeID = 0)
        {
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung thuộc tính";
                    var AttributeModelCreate = new ProductAttribute()
                    {
                        AttributeID = 0,
                        ProductID = productID
                    };
                    return View(AttributeModelCreate);

                case "edit":
                    ViewBag.Title = "Thay đổi thuộc tính";
                    var AttributeModelEdit = ProductDataService.GetAttribute(attributeID);
                    return View(AttributeModelEdit);

                case "delete":
                    ProductDataService.DeleteAttribute(attributeID);
                    return RedirectToAction($"Edit/{productID}"); //return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Edit");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Route("SaveAttribute/{ProductID}")]
        public ActionResult SaveAttribute(ProductAttribute data)
        {
            try
            {
                //Kiểm soát đầu vào
                if (string.IsNullOrWhiteSpace(data.AttributeName))
                    ModelState.AddModelError("AttributeName", "Tên thuộc tính không được để trống");
                if (string.IsNullOrWhiteSpace(data.AttributeValue))
                    ModelState.AddModelError("AttributeValue", "Giá trị không được để trống");
                if (data.DisplayOrder == 0 || data.DisplayOrder < 1)
                    ModelState.AddModelError("displayorder", "Thứ tự hiển thị không hợp lệ");

                if (!ModelState.IsValid)
                {
                    //ViewBag.Title = data.AttributeID != 0 ? "Cập thuộc tính mặt hàng" : "Bổ thuộc tính mặt hàng";
                    //var data2 = ProductDataService.GetAttribute(data.AttributeID);
                    return View("Attribute", data);
                }

                if (data.AttributeID == 0)
                {
                    ProductDataService.AddAttribute(data);
                }
                else
                {
                    ProductDataService.UpdateAttribute(data);
                }

                return RedirectToAction($"Edit/{data.ProductID}");
            }
            catch
            {
                // ghi lại log lỗi
                return Content("Có lỗi xảy ra, vui lòng thử lại sau!");
            }
        }
    }
}