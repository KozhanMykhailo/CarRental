using CarRental.Abstract;
using CarRental.Entities;
using CarRental.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarRental.Controllers
{
	public class AccountController : Controller
	{
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        public AccountController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(Registration model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userRepository.AllUsers.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    user = new User() { Email = model.Email, Password = model.Password, Lock = 0 };
                    Role userRole = await _roleRepository.Roles.FirstOrDefaultAsync(r => r.Name == "user");
                    if (userRole != null)
                    {
                        user.Role = userRole;
                        user.RoleId = userRole.Id;
                        _userRepository.SaveUser(user);
                        await Authenticate(user); // аутентификация
                        return RedirectToAction("Index", "Home");
                    }
                    return NotFound();
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            { 
                User user = await _userRepository.AllUsers
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user); // аутентификация
                    if (user.RoleId == _roleRepository.Roles.Where(w => w.Name == "admin").Select(s => s.Id).FirstOrDefault())
                        return RedirectToRoute(new { area = "Admin",controller = "Admin", action = "DefaultAction" });
                    if (user.RoleId == _roleRepository.Roles.Where(w => w.Name == "manager").Select(s => s.Id).FirstOrDefault())
                        return RedirectToRoute(new { area = "Manager", controller = "Manager", action = "Orders" });
                    if (user.RoleId == _roleRepository.Roles.Where(w => w.Name == "user").Select(s => s.Id).FirstOrDefault())
                    {
                        if (user.Lock == 1)
                            return NotFound();
                        HttpContext.Session.SetString("CurrentUserId", user.Id.ToString());

                        return RedirectToAction("Index", "Home");
                    }    
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
