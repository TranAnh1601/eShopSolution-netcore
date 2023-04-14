using eShopSolution.AdminApp.Services;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eShopSolution.AdminApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;
      
        public UserController(IUserApiClient userApiClient,
            IConfiguration configuration) 
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
           
        }

        public async Task<IActionResult>Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var session = HttpContext.Session.GetString("Token");
            var request = new GetUserPagingRequest()
            {
                BearerToken = session,
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _userApiClient.GetUsersPagings(request);
            //ViewBag.Keyword = keyword;
            //if (TempData["result"] != null)
            //{
            //    ViewBag.SuccessMsg = TempData["result"];
            //}
            //return View(data.ResultObj);
            return View(data);
        }
      

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            //logout session cu
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if(!ModelState.IsValid)
                return View(ModelState);
            //var result = await _userApiClient.Authenticate(request);
            //if (result.ResultObj == null)
            //{
            //    ModelState.AddModelError("", result.Message);
            //    return View();
            //}
           
            var token = await _userApiClient.Authenticate(request);
            // chuyen token sang userPrincipal
            var userPrincipal = this.ValidateToken(token); //result.ResultObj
            //authProperties of cookie
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = false // k ghi nho mat khau
            };
            HttpContext.Session.SetString("Token", token);
            await HttpContext.SignInAsync(
                       CookieAuthenticationDefaults.AuthenticationScheme,
                       userPrincipal,
                       authProperties);

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");

        }
        //ham giai ma token - chua thong tin dang nhap trong ClaimsPrincipal
        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true; // show event 

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
            validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

            return principal;
        }
    }
}
