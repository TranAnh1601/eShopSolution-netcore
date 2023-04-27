using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eShopSolution.ViewModels.System.Users
{
    public class LoginRequest
    {
        [Display(Name = "Tên tài khoản")]
        [Required(ErrorMessage = "Tên tài khoản không để trống!")]
        public string UserName { get; set; }

        [Display(Name = "mật khẩu")]
          [Required(ErrorMessage = "Mật khẩu không để trống!")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
