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
    public class LichTrinhLamViecsController : Controller
    {
        private dbQuanLyCongTyEntities db = new dbQuanLyCongTyEntities();

        // GET: LichTrinhLamViecs
        public ActionResult Index(String txtSearch, String filter)
        {
            var lichTrinhLamViec = db.LichTrinhLamViec.Include(l => l.NhanVien);
            if (txtSearch != null)
            {
                lichTrinhLamViec = lichTrinhLamViec.Where(nv => nv.TenCongViec.Contains(txtSearch) || nv.NhanVien.HoTen.Contains(txtSearch));
                ViewBag.txtSearch = txtSearch;
            }

            switch (filter)
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
            return View(lichTrinhLamViec.ToList());
        }

        // GET: LichTrinhLamViecs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichTrinhLamViec lichTrinhLamViec = db.LichTrinhLamViec.Find(id);
            if (lichTrinhLamViec == null)
            {
                return HttpNotFound();
            }
            return View(lichTrinhLamViec);
        }

        // GET: LichTrinhLamViecs/Create
        public ActionResult Create()
        {
            ViewBag.NguoiTao = new SelectList(db.NhanVien, "IDNhanVien", "username");
            return View();
        }

        // POST: LichTrinhLamViecs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDLichTrinh,TenCongViec,ThoiGian,Thumbnail,NguoiTao")] LichTrinhLamViec lichTrinhLamViec)
        {
            if (ModelState.IsValid)
            {
                db.LichTrinhLamViec.Add(lichTrinhLamViec);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NguoiTao = new SelectList(db.NhanVien, "IDNhanVien", "username", lichTrinhLamViec.NguoiTao);
            return View(lichTrinhLamViec);
        }

        // GET: LichTrinhLamViecs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichTrinhLamViec lichTrinhLamViec = db.LichTrinhLamViec.Find(id);
            if (lichTrinhLamViec == null)
            {
                return HttpNotFound();
            }
            ViewBag.NguoiTao = new SelectList(db.NhanVien, "IDNhanVien", "username", lichTrinhLamViec.NguoiTao);
            return View(lichTrinhLamViec);
        }

        // POST: LichTrinhLamViecs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDLichTrinh,TenCongViec,ThoiGian,Thumbnail,NguoiTao")] LichTrinhLamViec lichTrinhLamViec)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lichTrinhLamViec).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NguoiTao = new SelectList(db.NhanVien, "IDNhanVien", "username", lichTrinhLamViec.NguoiTao);
            return View(lichTrinhLamViec);
        }

        // GET: LichTrinhLamViecs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichTrinhLamViec lichTrinhLamViec = db.LichTrinhLamViec.Find(id);
            if (lichTrinhLamViec == null)
            {
                return HttpNotFound();
            }
            return View(lichTrinhLamViec);
        }

        // POST: LichTrinhLamViecs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LichTrinhLamViec lichTrinhLamViec = db.LichTrinhLamViec.Find(id);
            db.LichTrinhLamViec.Remove(lichTrinhLamViec);
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
