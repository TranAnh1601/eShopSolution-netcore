using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Exceptions;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;

        //thu vien identiti
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }
        public async Task<string> Authencate(LoginRequest request)
        {

            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return null;

            // if (user == null) return new ApiErrorResult<string>("Tài khoản không tồn tại");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);//false se khoa tai khoan
            if (!result.Succeeded)
            {
                return null;
            }       
              
        //    if (!result.Succeeded)
        //    {
        //        return new ApiErrorResult<string>("Đăng nhập không đúng");
        //    }
           var roles = await _userManager.GetRolesAsync(user); // lay list string
           var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, request.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        
        //return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
    }

        public async Task<PagedResult<UserVm>> GetUsersPaging(GetUserPagingRequest request)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(request.Keyword)) // neu khac rong
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword)
                 || x.PhoneNumber.Contains(request.Keyword)); //contains chua 1 trong cac ki tu
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new UserVm()
                {
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    UserName = x.UserName,
                    FirstName = x.FirstName,
                    Id = x.Id,
                    LastName = x.LastName
                }).ToListAsync();

            var pagedResult = new PagedResult<UserVm>()
            {
                TotalRecords = totalRow,
                //PageIndex = request.PageIndex,
                //PageSize = request.PageSize,
                Items = data
            };
            //return new ApiSuccessResult<PagedResult<UserVm>>(pagedResult);
            return pagedResult;
        }
      

        public async Task<bool> Register(RegisterRequest request)
        {
            //var user = await _userManager.FindByNameAsync(request.UserName);
            //if (user != null)
            //{
            //    return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
            //}
            //if (await _userManager.FindByEmailAsync(request.Email) != null)
            //{
            //    return new ApiErrorResult<bool>("Emai đã tồn tại");
            //}

           var user = new AppUser()
            {
                Dob = request.Dob,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return true;
               // return new ApiSuccessResult<bool>();
            }
            return false;
            //return new ApiErrorResult<bool>("Đăng ký không thành công");
        }
    }
}
