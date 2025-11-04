using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QL_PHONGGYM.AdminPortal.Models
{
    // Ánh xạ (map) tới bảng LopHoc
    [Table("LopHoc")]
    public class LopHoc
    {
        [Key]
        public int MaLop { get; set; }

        [Required(ErrorMessage = "Tên lớp không được để trống")]
        [StringLength(50)]
        public string TenLop { get; set; }

        // Khóa ngoại tới ChuyenMon
        public int MaCM { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal HocPhi { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime NgayBatDau { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime NgayKetThuc { get; set; }

        [Required]
        public int SoBuoi { get; set; }

        public int? SiSoToiDa { get; set; } // Cho phép null

        // ======================================================
        // Tạo mối quan hệ (Navigation Properties)
        // ======================================================

        // "Một Lớp Học này thuộc về MỘT Chuyên Môn"
        [ForeignKey("MaCM")]
        public virtual ChuyenMon ChuyenMon { get; set; }

        // "Một Lớp Học có THỂ CÓ NHIỀU Lượt Đăng Ký"
        public virtual ICollection<DangKyLop> DangKyLops { get; set; }

        // "Một Lớp Học có THỂ CÓ NHIỀU Lịch Lớp"
        public virtual ICollection<LichLop> LichLops { get; set; }

        public LopHoc()
        {
            this.DangKyLops = new HashSet<DangKyLop>();
            this.LichLops = new HashSet<LichLop>();
        }
    }
}