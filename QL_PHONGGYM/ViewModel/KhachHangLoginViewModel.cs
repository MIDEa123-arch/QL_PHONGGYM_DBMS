﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QL_PHONGGYM.ViewModel
{
    public class KhachHangLoginViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập.")]
        [StringLength(300, ErrorMessage = "Tên đăng nhập không được vượt quá 300 ký tự.")] // Matches DB limit
        [Display(Name = "Tên đăng nhập")]
        public string TenDangNhap { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [StringLength(1000, ErrorMessage = "Mật khẩu không được vượt quá 1000 ký tự.")] // Matches DB limit
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; }
    }
}