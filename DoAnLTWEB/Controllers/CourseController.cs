using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnLTWEB.Models;

namespace DoAnLTWEB.Controllers
{
    public class CourseController : Controller
    {
        QuanLyKhoaHocDataContext db = new QuanLyKhoaHocDataContext();
        //
        // GET: /Course/
        public ActionResult Course()
        {
            List<KhoaHoc> listCourse = new List<KhoaHoc>();
            listCourse = db.KhoaHocs.ToList();
            return View(listCourse);
        }
        public ActionResult AdminCourse()
        {
            List<KhoaHoc> listCourse = new List<KhoaHoc>();
            listCourse = db.KhoaHocs.ToList();
            return View(listCourse);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(KhoaHoc khoaHoc)
        {
            if (ModelState.IsValid)
            {
                db.KhoaHocs.InsertOnSubmit(khoaHoc);
                db.SubmitChanges();
                return RedirectToAction("AdminCourse");
            }
            return View();
        }
        public ActionResult Edit(string MaKH)
        {
            var KH = db.KhoaHocs.Where(kh => kh.MaKH == MaKH).FirstOrDefault();
            return View(KH);
        }

        [HttpPost]
        public ActionResult Edit(KhoaHoc kh)
        {
            if (ModelState.IsValid)
            {
                var KH = db.KhoaHocs.Where(k => k.MaKH == kh.MaKH).FirstOrDefault();
                if (KH != null)
                {
                    KH.TenKH = kh.TenKH;
                    KH.MoTaKH = kh.MoTaKH;
                    KH.GiangVien = kh.GiangVien;
                    KH.NgayBD = kh.NgayBD;
                    KH.NgayKT = kh.NgayKT;
                    KH.GiaKhoaHoc = kh.GiaKhoaHoc;
                    KH.TenMonHoc = kh.TenMonHoc;
                    KH.Picture = kh.Picture;
                    db.SubmitChanges();
                    return RedirectToAction("AdminCourse");
                }
            }

            return View(kh);
        }
        public ActionResult Delete(string MaKH)
        {
            var KH = db.KhoaHocs.Where(kh => kh.MaKH == MaKH).FirstOrDefault();
            return View(KH);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string MaKH)
        {
            var KH = db.KhoaHocs.Where(kh => kh.MaKH == MaKH).FirstOrDefault();
            if (KH != null)
            {
                db.KhoaHocs.DeleteOnSubmit(KH);
                db.SubmitChanges();
            }

            return RedirectToAction("AdminCourse");
        }
        public ActionResult CourseWithSubjectTitle(string TenMH)
        {
            List<KhoaHoc> listCourse = new List<KhoaHoc>();
            ViewBag.TenMH = null;
            if(TenMH != null)
            {
                listCourse = db.KhoaHocs.Where(c => c.TenMonHoc == TenMH).ToList();
                ViewBag.TenMH = TenMH;
            }
            else
            {
                return RedirectToAction("Course");
            }
            return View(listCourse);
        }

        public ActionResult SideBar()
        {
            var listName = db.KhoaHocs.Take(10).ToList();
            return View(listName);
        }

    }
}