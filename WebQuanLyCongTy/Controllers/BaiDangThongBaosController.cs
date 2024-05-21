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
    public class BaiDangThongBaosController : Controller
    {
        private dbQuanLyCongTyEntities db = new dbQuanLyCongTyEntities();

        // GET: BaiDangThongBaos
        public ActionResult Index(String txtSearch, String filter)
        {
            var baiDangThongBao = db.BaiDangThongBao.Include(b => b.NhanVien).Include(b => b.TheLoai1);
            if (txtSearch != null)
            {
                baiDangThongBao = baiDangThongBao.Where(bd => bd.TieuDe.Contains(txtSearch) || bd.NhanVien.HoTen.Contains(txtSearch));
                ViewBag.txtSearch = txtSearch;
            }

            switch (filter)
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
                    // No filter applied
                    break;
            }
            return View(baiDangThongBao.ToList());
        }

        // GET: BaiDangThongBaos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiDangThongBao baiDangThongBao = db.BaiDangThongBao.Find(id);
            if (baiDangThongBao == null)
            {
                return HttpNotFound();
            }
            return View(baiDangThongBao);
        }

        // GET: BaiDangThongBaos/Create
        public ActionResult Create()
        {
            ViewBag.NguoiTao = new SelectList(db.NhanVien, "IDNhanVien", "username");
            ViewBag.TheLoai = new SelectList(db.TheLoai, "IDTheLoai", "TenTheLoai");
            return View();
        }

        // POST: BaiDangThongBaos/Create
        [HttpPost]
        public ActionResult Create(BaiDangThongBao baiDangThongBao, HttpPostedFileBase anh)
        {
            if (anh != null)
            {
                String pathroot = Server.MapPath("/img/BaiDang/");
                String pathImage = pathroot + anh.FileName;
                anh.SaveAs(pathImage);
                baiDangThongBao.Thumbnail = anh.FileName;
            }
            db.BaiDangThongBao.Add(baiDangThongBao);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: BaiDangThongBaos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiDangThongBao baiDangThongBao = db.BaiDangThongBao.Find(id);
            if (baiDangThongBao == null)
            {
                return HttpNotFound();
            }
            ViewBag.lt = baiDangThongBao;
            return View(baiDangThongBao);
        }

        // POST: BaiDangThongBaos/Edit/5
        [HttpPost]
        public ActionResult Edit(BaiDangThongBao baiDangThongBao, HttpPostedFileBase anh)
        {
            if (baiDangThongBao.IDBaiDang == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiDangThongBao baiDangThongBaoFind = db.BaiDangThongBao.Find(baiDangThongBao.IDBaiDang);
            baiDangThongBaoFind.TieuDe = baiDangThongBao.TieuDe;
            baiDangThongBaoFind.NoiDung = baiDangThongBao.NoiDung;
            baiDangThongBaoFind.NguoiTao = baiDangThongBao.NguoiTao;
            baiDangThongBaoFind.TheLoai = baiDangThongBao.TheLoai;
            baiDangThongBaoFind.ThoiGianTao = baiDangThongBao.ThoiGianTao;
            if (anh != null)
            {
                String pathroot = Server.MapPath("/img/BaiDang/");
                String pathImage = pathroot + anh.FileName;
                anh.SaveAs(pathImage);
                baiDangThongBaoFind.Thumbnail = anh.FileName;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: BaiDangThongBaos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiDangThongBao baiDangThongBao = db.BaiDangThongBao.Find(id);
            if (baiDangThongBao == null)
            {
                return HttpNotFound();
            }
            return View(baiDangThongBao);
        }

        // POST: BaiDangThongBaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BaiDangThongBao baiDangThongBao = db.BaiDangThongBao.Find(id);
            db.BaiDangThongBao.Remove(baiDangThongBao);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
