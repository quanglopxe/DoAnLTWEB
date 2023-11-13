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
	}
}