using DoAnLTWEB.Models;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using System;
using System.Web.Mvc;
using System.Linq;
using System.Text.RegularExpressions;

namespace DoAnLTWEB.Controllers
{
    public class LessonController : Controller
    {
        QuanLyKhoaHocDataContext db = new QuanLyKhoaHocDataContext();
        public ActionResult Lesson(string maKH)
        {
            try
            {
                List<BaiGiang> listBaiGiang = db.BaiGiangs.Where(c => c.MaKH == maKH).ToList();
                return View(listBaiGiang);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "User", new { error = ex.Message });
            }
        }
        public ActionResult LessonDetail(string maBG)
        {
            try
            {
                var baiGiang = db.BaiGiangs.FirstOrDefault(m => m.MaBG == maBG);
                baiGiang.Video = GetEmbedYouTubeLink(baiGiang.Video);
                if (baiGiang == null)
                {
                    return RedirectToAction("Error", "User", new { error = "Không tìm thấy bài giảng " + maBG });
                }

                return View(baiGiang);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "User", new { error = ex.Message });
            }
        }
        private string GetEmbedYouTubeLink(string link)
        {
            Regex regex = new Regex(@"(?<=v=)[a-zA-Z0-9_-]*");

            Match match = regex.Match(link);
            string newLink = "https://www.youtube.com/";
            if (match.Success)
            {
                newLink += "embed/" + match.Groups[0].Value;
            }

            return newLink;
        }
    }
}