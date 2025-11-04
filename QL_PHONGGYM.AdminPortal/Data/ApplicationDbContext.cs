using QL_PHONGGYM.AdminPortal.Models;
using System.Data.Entity;

namespace QL_PHONGGYM.AdminPortal.Data
{
    public class ApplicationDbContext : DbContext
    {
        // "FullConnection" là tên của chuỗi kết nối trong Web.config
        // mà chúng ta đã thêm ở bước trước (chuỗi kết nối có User/Pass)
        public ApplicationDbContext() : base("name=FullConnection")
        {
        }

        // Khai báo cho DbContext biết về Model/Bảng KhachHang
        // Tên "KhachHangs" sẽ được EF tự động ánh xạ (map) 
        // tới bảng "KhachHang" mà chúng ta định nghĩa trong Model
        public DbSet<KhachHang> KhachHang { get; set; }

        // Thêm các DbSet khác cho các bảng khác (NhanVien, GoiTap...) ở đây
        // Ví dụ:
        // public DbSet<NhanVien> NhanViens { get; set; }
        // public DbSet<GoiTap> GoiTaps { get; set; }

        public DbSet<LoaiKhachHang> LoaiKhachHang { get; set; }

        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public DbSet<HINHANH> HINHANHs { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<ChucVu> ChucVus { get; set; }
        public DbSet<ChuyenMon> ChuyenMons { get; set; }
        public DbSet<NhanVienChuyenMon> NhanVienChuyenMons { get; set; }

        public DbSet<CheckIn> CheckIns { get; set; }
        public DbSet<GoiTap> GoiTaps { get; set; }


        // Cấu hình nâng cao (cho Khóa chính phức tạp)
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Cấu hình Khóa chính TỔ HỢP (Composite Key) cho bảng NhanVienChuyenMon
            // (Bảng này có 2 khóa chính là MaNV và MaCM)
            modelBuilder.Entity<NhanVienChuyenMon>()
                .HasKey(nvc => new { nvc.MaNV, nvc.MaCM });

            // (Bạn có thể thêm các cấu hình Fluent API khác ở đây nếu cần)

            base.OnModelCreating(modelBuilder);
        } 
        public DbSet<LopHoc> LopHocs { get; set; }
        public DbSet<DangKyLop> DangKyLops { get; set; }
        public DbSet<LichLop> LichLops { get; set; }

        
         public DbSet<LichTapPT> LichTapPTs { get; set; }
         public DbSet<DangKyPT> DangKyPTs { get; set; }
    }
}