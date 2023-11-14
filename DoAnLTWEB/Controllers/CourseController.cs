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