using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebQuanLyCongTy.Models;

namespace WebQuanLyCongTy.Controllers
{
    public class HomeController : Controller
    {
        private dbQuanLyCongTyEntities db = new dbQuanLyCongTyEntities();

        public ActionResult Index(string txtSearch, string filterNotica, string filterSchedule)
        {
            // Lấy danh sách bài đăng thông báo
            var baiDangThongBao = db.BaiDangThongBao.Include(b => b.NhanVien).Include(b => b.TheLoai1).AsQueryable();

            if (!string.IsNullOrEmpty(txtSearch))
            {
                baiDangThongBao = baiDangThongBao.Where(bd => bd.TieuDe.Contains(txtSearch) || bd.NhanVien.HoTen.Contains(txtSearch));
                ViewBag.txtSearch = txtSearch;
            }

            switch (filterNotica)
            {
                case "Today":
                    var today = DateTime.Today;
                    baiDangThongBao = baiDangThongBao.Where(l => DbFunctions.TruncateTime(l.ThoiGianTao) == today);
                    break;
                case "ThisWeek":
                    var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
                    var endOfWeek = startOfWeek.AddDays(7);
                    baiDangThongBao = baiDangThongBao.Where(l => DbFunctions.TruncateTime(l.ThoiGianTao) >= startOfWeek && DbFunctions.TruncateTime(l.ThoiGianTao) < endOfWeek);
                    break;
                case "ThisMonth":
                    var startOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    var endOfMonth = startOfMonth.AddMonths(1);
                    baiDangThongBao = baiDangThongBao.Where(l => DbFunctions.TruncateTime(l.ThoiGianTao) >= startOfMonth && DbFunctions.TruncateTime(l.ThoiGianTao) < endOfMonth);
                    break;
                default:
                    break;
            }

            // Lấy danh sách lịch trình làm việc
            var lichTrinhLamViec = db.LichTrinhLamViec.Include(l => l.NhanVien);
            if (txtSearch != null)
            {
                lichTrinhLamViec = lichTrinhLamViec.Where(nv => nv.TenCongViec.Contains(txtSearch) || nv.NhanVien.HoTen.Contains(txtSearch));
                ViewBag.txtSearch = txtSearch;
            }

            switch (filterSchedule)
            {
                case "Today":
                    var today = DateTime.Today;
                    lichTrinhLamViec = lichTrinhLamViec.Where(l => DbFunctions.TruncateTime(l.ThoiGian) == today);
                    break;
                case "ThisWeek":
                    var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
                    var endOfWeek = startOfWeek.AddDays(7);
                    lichTrinhLamViec = lichTrinhLamViec.Where(l => DbFunctions.TruncateTime(l.ThoiGian) >= startOfWeek && DbFunctions.TruncateTime(l.ThoiGian) < endOfWeek);
                    break;
                case "ThisMonth":
                    var startOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    var endOfMonth = startOfMonth.AddMonths(1);
                    lichTrinhLamViec = lichTrinhLamViec.Where(l => DbFunctions.TruncateTime(l.ThoiGian) >= startOfMonth && DbFunctions.TruncateTime(l.ThoiGian) < endOfMonth);
                    break;
                default:
                    // No filter applied
                    break;
            }

            var model = new IndexViewModel
            {
                BaiDangThongBao = baiDangThongBao.ToList(),
                LichTrinhLamViec = lichTrinhLamViec.ToList(),
                FilterNotica = filterNotica,
                FilterSchedule = filterSchedule,
                TxtSearch = txtSearch
            };

            return View(model);
        }

        public class IndexViewModel
        {
            public List<BaiDangThongBao> BaiDangThongBao { get; set; }
            public List<LichTrinhLamViec> LichTrinhLamViec { get; set; }
            public string FilterNotica { get; set; }
            public string FilterSchedule { get; set; }
            public string TxtSearch { get; set; }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}