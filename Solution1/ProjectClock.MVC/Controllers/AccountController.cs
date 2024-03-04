using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectClock.BusinessLogic.Dtos.AccountDtos;
using ProjectClock.BusinessLogic.Services;
using System.Security.Claims;
using ProjectClock.MVC.Extensions;


namespace ProjectClock.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var resultDto = await _accountService.LoginAccount(dto);

            if (!resultDto.LoginWasSuccessful)
            {
                return View(dto);
            }

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(resultDto.ClaimsIdentity),
                resultDto.AuthProp);

            return RedirectToAction("index", "Project");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            
            var resultDto = await _accountService.RegisterAccount(dto);
            _mapper.Map(resultDto, dto);

            if (resultDto.RegistrationFailed)
            {
                return View(dto);
            }

            return RedirectToAction("Login");
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("index", "Home");
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> EditEmail()
        {
            if (!HttpContext.User.Claims.TryGetAuthenticatedUserId(out var userId))
            {
                return RedirectToAction("Index", "Home");
            }

            var accountEmail = await _accountService.GetAccountEmail(userId);

            var dto = new EditEmailDto
            {
                Id = userId,
                CurrentEmail = accountEmail
            };

            return View(dto);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> EditEmail(EditEmailDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var resultDto = await _accountService.EditAccountEmail(dto);

            if (resultDto.EditEmailFailed)
            {
                return View(resultDto);
            }

            return RedirectToAction("Details", "User");
        }

        [Authorize(Roles = "User")]
        public ActionResult EditPassword()
        {
            if (!HttpContext.User.Claims.TryGetAuthenticatedUserId(out var userId))
            {
                return RedirectToAction("Index", "Home");
            }

            var dto = new EditPasswordDto
            {
                Id = userId
            };

            return View(dto);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> EditPassword(EditPasswordDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
          
            var resultDto = await _accountService.EditAccountPassword(dto);

            if (resultDto.EditPasswordFailed)
            {
                return View(resultDto);
            }

            return RedirectToAction("Details", "User");
        }

        [Authorize(Roles = "User")]
        public IActionResult Delete()
        {
            if (!HttpContext.User.Claims.TryGetAuthenticatedUserId(out var userId))
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new DeleteAccountModel()
            {
                UserId = userId
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete(DeleteAccountModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = _mapper.Map<DeleteAccountModel, DeleteAccountDto>(model);
            var deletionSuccessful = await _accountService.DeleteAccount(dto);

            if (deletionSuccessful)
            {
                return RedirectToAction("Logout");
            }

            model.DeleteAccountFailed = true;
            return View(model);
        }
    }
}
