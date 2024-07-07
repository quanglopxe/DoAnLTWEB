using DoAnLTWEB.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace DoAnLTWEB.Controllers
{
    public class UserController : Controller
    {
        QuanLyKhoaHocDataContext db = new QuanLyKhoaHocDataContext();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(NguoiDung nguoiDung)
        {
            try
            {
                NguoiDung nd = db.NguoiDungs.FirstOrDefault(t => t.TenND == nguoiDung.TenND && t.MatKhau == nguoiDung.MatKhau);
                Session["User"] = nd;
                if (nd == null)
                {
                    return RedirectToAction("Error", "User", new { error = "Tên tài khoản hoặc mật khẩu không đúng." });
                }
                if(nd!=null && nd.Roles==true)
                {
                    return RedirectToAction("Index1", "Home");
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "User", new { error = ex.Message });
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(NguoiDung nguoiDung)
        {
            string maND = "ND2023" + String.Format("{0:D4}", db.NguoiDungs.Count() + 1);
            try
            {
                nguoiDung.MaND = maND;
                db.NguoiDungs.InsertOnSubmit(nguoiDung);
                db.SubmitChanges();
                return RedirectToAction("Login", "User");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "User", new { error = ex.Message });
            }
        }
        public ActionResult EditProfile()
        {
            NguoiDung nguoiDung = (NguoiDung)Session["User"];
            if (nguoiDung == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View(nguoiDung);
        }
        [HttpPost]
        public ActionResult EditProfile(NguoiDung nguoiDung)
        {
            try
            {
                NguoiDung nd = db.NguoiDungs.FirstOrDefault(t => t.MaND == nguoiDung.MaND);
                nd.HoTen = nguoiDung.HoTen;
                nd.Email = nguoiDung.Email;
                nd.DienThoai = nguoiDung.DienThoai;
                nd.NgaySinh = nguoiDung.NgaySinh;
                Session["User"] = nd;
                db.SubmitChanges();
                return RedirectToAction("Courses", "Courses");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "User", new { error = ex.Message });
            }
        }
        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Login", "User");
        }
        public ActionResult Error(string error = "")
        {
            ViewBag.ErrorMessage = error;
            return View();
        }
    }
}