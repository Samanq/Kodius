using Kodius.Presentation.WebApp.Models;
using Kodius.Presentation.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kodius.Presentation.WebApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepositoryService _repository;
        private readonly IAuthtenticationService _authtenticationService;
        private readonly ITokenService _tokenService;

        public UsersController(
            IUserRepositoryService repository,
            IAuthtenticationService authtenticationService,
            ITokenService tokenService)
        {
            _repository = repository;
            _authtenticationService = authtenticationService;
            _tokenService = tokenService;
        }


        public async Task<IActionResult> Index()
        {
            var users = await _repository.GetAll();

            return View(users);
        }

        public IActionResult Create()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterModel model)
        {
            if (await _repository.GetByEmail(model.Email) != null)
            {
                ViewBag.Error = "User already exist";
                return View(model);
            }

            if (ModelState.IsValid)
            {
                _authtenticationService.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

                var user = new User
                {
                    Email = model.Email,
                    HashSalt = passwordSalt,
                    HashPassword = passwordHash
                };

                await _repository.Add(user);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _repository.GetByEmail(model.Email);

            if (user == null)
            {
                ViewBag.Error = "User not found";
                return View(model);
            }

            if (!_authtenticationService.VerifyPasswordHash(model.Password, user.HashPassword, user.HashSalt))
            {
                ViewBag.Error = "Wrong Password";
                return View(model);
            }

            var token = _tokenService.CreateToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            SetRefreshToken(refreshToken, user);

            //if (model.Email.ToLower() != user.Username)
            //    return BadRequest("User not found.");

            //if (!_authtenticationService.VerifyPasswordHash(dto.Password, user.PasswordHash, user.PasswordSalt))
            //    return BadRequest("Wrong Password");

            //var token = _tokenService.CreateToken(user);

            //var refreshToken = _tokenService.GenerateRefreshToken();
            //SetRefreshToken(refreshToken);
            //return Ok(token);
            return View(model);
        }
        private void SetRefreshToken(RefreshToken refreshToken, User user)
        {
            var cookieOption = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.ExpiryDate
            };

            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOption);
            user.RefreshToken = refreshToken.Token;
            user.RefreshTokenExpiryDate = refreshToken.ExpiryDate;
        }
    }
}
