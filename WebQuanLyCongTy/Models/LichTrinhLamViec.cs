﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebQuanLyCongTy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class LichTrinhLamViec
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LichTrinhLamViec()
        {
            this.PhongBan = new HashSet<PhongBan>();
        }
    
        public int? IDLichTrinh { get; set; }
        public string TenCongViec { get; set; }
        public Nullable<System.DateTime> ThoiGian { get; set; }
        public string Thumbnail { get; set; }
        public Nullable<int> NguoiTao { get; set; }
    
        public virtual NhanVien NhanVien { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhongBan> PhongBan { get; set; }
    }
}
