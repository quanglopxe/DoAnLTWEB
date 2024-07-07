using DoAnLTWEB.Models;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using System;
using System.Web.Mvc;
using System.Linq;
using System.Text.RegularExpressions;
//using DoAnLTWEB.ViewModel;

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
                ViewData["IsRegisterCourse"] = false;
                if (listBaiGiang.Count > 0)
                {
                    ViewData["KhoaHoc"] = listBaiGiang.First().KhoaHoc;
                    NguoiDung nguoiDung = (NguoiDung)Session["User"];
                    if (nguoiDung != null)
                    {
                        DKyKhoaHoc dKyKhoaHoc = db.DKyKhoaHocs.FirstOrDefault(t => t.MaND == nguoiDung.MaND);
                        if(dKyKhoaHoc != null)
                        {
                            ViewData["IsRegisterCourse"] = true;
                        }
                    }

                }
                return View(listBaiGiang);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "User", new { error = ex.Message });
            }
        }
        //public ActionResult ShowComment(string MaKH)
        //{

        //    if (Session["User"] != null)
        //    {
        //        // Lấy thông tin người dùng hiện tại từ Session
        //        var user = (NguoiDung)Session["User"];

        //        // Truy vấn danh sách bình luận của khóa học dựa trên MaKH và MaND
        //        var comments = from c in db.rateKhoaHocs
        //                       where c.MaKH == MaKH && c.MaND == user.MaND
        //                       select c;

        //        // Hiển thị thông tin bình luận
        //        var viewModel = new MyViewModel
        //        {
        //            rateKhoaHocs = comments
        //        };

        //        return View(viewModel.rateKhoaHocs);
        //    }
        //    else
        //    {
        //        // Người dùng chưa đăng nhập, xử lý tùy ý (chẳng hạn, chuyển hướng đến trang đăng nhập)
        //        return RedirectToAction("Login", "User");
        //    }
        //}

        //[HttpPost]
        //public ActionResult CommentOnKhoaHoc(MyViewModel vm)
        //{
        //    // Kiểm tra người dùng đã đăng nhập hay chưa bằng Session
        //    if (Session["User"] != null)
        //    {
        //        // Lấy thông tin người dùng hiện tại từ Session
        //        var user = (NguoiDung)Session["User"];
        //        string khoaHocId = vm.MaKH;
        //        string noiDung = vm.NewComment;

        //        // Tạo đối tượng Comment và gán giá trị
        //        rateKhoaHoc newComment = new rateKhoaHoc
        //        {
        //            Mota = noiDung,
        //            rateDate = DateTime.Now,
        //            MaKH = khoaHocId,
        //            MaND = user.MaND
        //        };

        //        // Thêm đối tượng vào bảng Comment
        //        db.rateKhoaHocs.InsertOnSubmit(newComment);

        //        try
        //        {
        //            // Lưu thay đổi vào cơ sở dữ liệu
        //            db.SubmitChanges();

        //            // Redirect hoặc trả về JSON phản hồi thành công
        //            return RedirectToAction("ShowComment", "Lesson", new { id = khoaHocId });
        //        }
        //        catch (Exception ex)
        //        {
        //            // Xử lý ngoại lệ (log, hiển thị lỗi, v.v.)
        //            ViewBag.ErrorMessage = "Bình luận không thành công: " + ex.Message;
        //            return View("Error");
        //        }
        //        finally
        //        {
        //            // Đảm bảo giải phóng tài nguyên
        //            db.Dispose();
        //        }
        //    }
        //    else
        //    {
        //        // Người dùng chưa đăng nhập, xử lý tùy ý (chẳng hạn, chuyển hướng đến trang đăng nhập)
        //        return RedirectToAction("Login", "User");
        //    }
        //}
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