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
            if (_repository.GetByEmail(model.Email) != null)
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
    }
}
