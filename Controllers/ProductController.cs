using BaiTH1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace BaiTH1.Controllers
{
    public class ProductController : Controller
    {
        DbTh1Entities db = new DbTh1Entities();


        #region Dang Nhap va Dang Ky
        //Register
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(User newUser)
        {
            db.Users.Add(newUser);
            db.SaveChanges();
            return RedirectToAction("ListUser");
        }

        //Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User model)
        {
            // Kiểm tra thông tin đăng nhập với cơ sở dữ liệu
            var user = db.Users.FirstOrDefault(u => u.UserName == model.UserName && u.Passwords == model.Passwords);

            if (user != null)
            {
                // Đăng nhập thành công
                // Điều hướng đến trang chính hoặc trang mong muốn
                return RedirectToAction("Home");
            }
            else
            {
                // Đăng nhập thất bại, thêm thông báo lỗi
                ModelState.AddModelError("", "Thông tin đăng nhập không đúng");
            }
            // Nếu đăng nhập thất bại, hiển thị lại form đăng nhập với thông báo lỗi
            return View(model);
        }
        #endregion




        public ActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(SanPham newObj)
        {
            db.SanPhams.Add(newObj);
            db.SaveChanges();
            return RedirectToAction("CreateProduct");
        }

        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                // Lấy tên file
                string fileName = Path.GetFileName(file.FileName);

                // Lưu file vào thư mục trên server
                var relativepath = Path.Combine("~/Content/Img/", fileName);
                file.SaveAs(relativepath);
                // Lưu tên file vào database
               
                    var model = new SanPham();
                    model.imagePath = fileName;
                    db.SanPhams.Add(model);
                    db.SaveChanges();
 

                // Redirect hoặc trả về thông báo thành công
                return RedirectToAction("Index");
            }

            // Trả về thông báo lỗi nếu không có file được chọn
            ModelState.AddModelError("", "Vui lòng chọn một file hình");
            return View();
        }


        public ActionResult ListPrpduct()
        {
            return View(db.SanPhams.ToList());
        }


       
    }
}