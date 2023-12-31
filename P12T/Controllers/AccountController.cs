﻿using P12T.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Razor.Generator;

namespace P12T.Controllers
{
    public class AccountController : Controller
    {
        private P12TEntities db = new P12TEntities();

        // LOGIN
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Account model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Accounts.FirstOrDefault(a => a.Email == model.Email && a.Password == model.Password);
                if (user != null)
                {
                    if (db.Accounts.FirstOrDefault(a => a.Email == model.Email && a.Password == model.Password && a.Type == "Admin") != null)
                    {
                        var fullName = user.FullName;
                        var type = user.Type;

                        Session["FullName"] = fullName;
                        Session["Type"] = type;

                        return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
                    } else
                    {
                        var fullName = user.FullName;
                        var type = user.Type;

                        Session["FullName"] = fullName;
                        Session["Type"] = type;

                        return RedirectToAction("Index", "Home");
                    }  
                }
                else
                {
                    ModelState.AddModelError("Password", "Email hoặc mật khẩu không hợp lệ.");
                }
            }
            return View(model);
        }


        //REGISTER
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Account obj)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem địa chỉ email có đúng định dạng không
                if (!IsValidEmail(obj.Email))
                {
                    ModelState.AddModelError("Email", "Địa chỉ email không hợp lệ.");
                    return View(obj); // Hiển thị form đăng ký với thông báo lỗi
                }

                // Kiểm tra xem địa chỉ email đã tồn tại trong cơ sở dữ liệu hay chưa
                var existingAccount = db.Accounts.FirstOrDefault(a => a.Email == obj.Email);

                if (existingAccount != null)
                {
                    ModelState.AddModelError("Email", "Địa chỉ email đã được sử dụng cho một tài khoản khác.");
                    return View(obj); // Hiển thị form đăng ký với thông báo lỗi
                }
                else
                {
                    // Kiểm tra xem mật khẩu có ít nhất 6 ký tự hay không
                    if (obj.Password.Length < 6)
                    {
                        ModelState.AddModelError("Password", "Mật khẩu phải có ít nhất 6 ký tự.");
                        return View(obj); // Hiển thị form đăng ký với thông báo lỗi
                    }

                    // Nếu địa chỉ email hợp lệ và chưa tồn tại, thêm tài khoản mới vào cơ sở dữ liệu
                    db.Accounts.Add(obj);
                    db.SaveChanges();
                    return RedirectToAction("Register");
                }
            }
            return View(obj);
        }

        // Phương thức để kiểm tra địa chỉ email có hợp lệ hay không bằng Regular Expression
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
