using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QL_PHONGGYM.AdminPortal.Models
{
    // Ánh xạ (map) tới bảng LichLop
    [Table("LichLop")]
    public class LichLop
    {
        [Key]
        public int MaLichLop { get; set; }

        // Khóa ngoại tới LopHoc
        public int MaLop { get; set; }

        // Khóa ngoại tới NhanVien (người dạy)
        public int MaNV { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime NgayHoc { get; set; }

        [Required]
        public TimeSpan GioBatDau { get; set; }

        [Required]
        public TimeSpan GioKetThuc { get; set; }

        [StringLength(20)]
        public string TrangThai { get; set; }

        // ======================================================
        // Tạo mối quan hệ (Navigation Properties)
        // ======================================================

        [ForeignKey("MaLop")]
        public virtual LopHoc LopHoc { get; set; }

        [ForeignKey("MaNV")]
        public virtual NhanVien NhanVien { get; set; }
    }
}