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
    public class NhanViensController : Controller
    {
        private dbQuanLyCongTyEntities db = new dbQuanLyCongTyEntities();

        // GET: NhanViens
        public ActionResult Index(String txtSearch)
        {
            var nhanVien = db.NhanVien.Include(n => n.ChucVu).Include(n => n.PhongBan);
            if (!String.IsNullOrEmpty(txtSearch))
            {
                nhanVien = nhanVien.Where(nv => nv.HoTen.Contains(txtSearch) || nv.username.Contains(txtSearch) || nv.Email.Contains(txtSearch) || nv.SDT.Contains(txtSearch));
                ViewBag.txtSearch = txtSearch;
            }
            return View(nhanVien.ToList());
        }

        // GET: NhanViens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanVien.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // GET: NhanViens/Create
        public ActionResult Create()
        {
            ViewBag.MaChucVu = new SelectList(db.ChucVu, "IDChucVu", "TenChucVu");
            ViewBag.IDPhongBan = new SelectList(db.PhongBan, "IDPhongBan", "TenPhongBan");
            return View();
        }

        // POST: NhanViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDNhanVien,username,password,HoTen,SDT,Email,NgaySinh,status,IDPhongBan,MaChucVu")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.NhanVien.Add(nhanVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaChucVu = new SelectList(db.ChucVu, "IDChucVu", "TenChucVu", nhanVien.MaChucVu);
            ViewBag.IDPhongBan = new SelectList(db.PhongBan, "IDPhongBan", "TenPhongBan", nhanVien.IDPhongBan);
            return View(nhanVien);
        }

        // GET: NhanViens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanVien.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaChucVu = new SelectList(db.ChucVu, "IDChucVu", "TenChucVu", nhanVien.MaChucVu);
            ViewBag.IDPhongBan = new SelectList(db.PhongBan, "IDPhongBan", "TenPhongBan", nhanVien.IDPhongBan);
            return View(nhanVien);
        }

        // POST: NhanViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDNhanVien,username,password,HoTen,SDT,Email,NgaySinh,status,IDPhongBan,MaChucVu")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaChucVu = new SelectList(db.ChucVu, "IDChucVu", "TenChucVu", nhanVien.MaChucVu);
            ViewBag.IDPhongBan = new SelectList(db.PhongBan, "IDPhongBan", "TenPhongBan", nhanVien.IDPhongBan);
            return View(nhanVien);
        }

        // GET: NhanViens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanVien.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: NhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NhanVien nhanVien = db.NhanVien.Find(id);
            db.NhanVien.Remove(nhanVien);
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
