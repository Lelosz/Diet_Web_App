using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using WebAPI.Data;
using WebAPI.Models;
using System;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }



        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserModel userModel)
        {

            UserModel existingEmail = _context.Users.FirstOrDefault(u => u.EmailAdress.ToLower() == userModel.EmailAdress.ToLower());
            UserModel existingUsername = _context.Users.FirstOrDefault(u => u.Username.ToLower() == userModel.Username.ToLower());
            if (existingEmail != null | existingUsername != null) 
            {
                return BadRequest("Email jest juz zajety.");
            }

            UserModel newUser = new UserModel
            {
                Username = userModel.Username,
                Password = userModel.Password,
                EmailAdress = userModel.EmailAdress,
                Role = userModel.Role,
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("token");

            return Ok(new
            {
                message = "success"
            });
        }


        [HttpGet("Administrator")]
        [Authorize(Roles = "Administrator")]
        public IActionResult AdminsEndpoint()
        {
            var currentUser = GetCurrentUser();
            return Ok(currentUser);
        }

        

        [HttpGet("User")]
        [Authorize(Roles = "User")]
        public IActionResult retrieveUserInformation()
        {
            //var token = Request.Cookies["jwt"];
            //if (token != null)
            //{
            //    var currentUser = GetCurrentUser(token);
            //    return Ok(currentUser);
            //}
            //return BadRequest("brak tokenu");

            var currentUser = GetCurrentUser();
            return Ok(currentUser);
        }

        [HttpGet("Public")]
        public IActionResult Public()
        {
            return Ok();
        }

        private UserModel GetCurrentUser()
        {
            //var token = Request.Cookies["jwt"];
            //var identity = new JwtSecurityTokenHandler().ReadJwtToken(token);

            //if (identity != null)
            //{
            //    var userClaims = identity.Claims;
            //    return new UserModel
            //    {
            //        Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
            //        EmailAdress = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
            //        Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
            //    };
            //}
            //return null;

            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new UserModel
                {
                    Id = Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value),
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value,
                    EmailAdress = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }
        }
}
