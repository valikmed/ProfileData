using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application;
using Domain.Abstractions.Services;
using Domain.Entities;
using Infrastructure.Authorization;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProfileData.Controllers
{
    //[Authorize] // Тут ми позначаємо що даний метод потребує авторизації
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ProfileDataContext _context;
        private readonly IUserService _userService;


        public AccountController(ProfileDataContext profileDataContext, IUserService userService)
        {
            _context = profileDataContext;
            _userService = userService;

        }

        // POST /token request
        // Endpoint for Authentication
        [HttpPost("/token")]
        public IActionResult Token(string username, string password)
        {
            // Get Identity from our GetIdentity function
            var identity = GetIdentity(username, password);

            // If no identity is found, return null
            if (identity == null) return BadRequest(new { errorText = "Invalid username or password." });

            // Create JWT token
            // Use data from our AuthOptions class
            var now = DateTime.UtcNow; // Initialize the now variable
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // Create new object to return token info
            return Json(new
            {
                access_token = encodedJwt,
                username = identity.Name
            });
        }

        [Authorize] // Тут ми позначаємо що даний метод потребує авторизації
        [HttpGet("/api/do")]
        public void doSmth()
        {
            var userName = User.Identity.Name; // Повертає string? назви авторизованого користувача
            var isAuth = User.Identity.IsAuthenticated; // Повертає true якщо користувач успішно ввійшов у систему
        }


        private ClaimsIdentity GetIdentity(string username, string password)
        {
            // Get user that matches Login and Password
            // This can be any data source - a database, a plain list, etc.
            User user = _context.Users.FirstOrDefault(x => x.Username == username && x.HashedPassword == password);

            // If user is not found, return null
            if (user == null) return null;

            // Create a list of Claim objects
            // Do not pass the password
            var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username), // Define Login
            //new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)   // Define Role
        };

            // Return Identity
            // claims - List of Claim objects.
            // "Token" - Let it be as it is.
            // Last two parameters - Login key and User Role key
            return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }

}

