using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentsCommunity.Models;
using System.Linq;


namespace StudentsCommunity.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly StudentCommunityContext _context;

        public AuthController(StudentCommunityContext context)
        {
            _context = context;
        }

 

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
          
            return new string[] {"value1", "value2"};
        }
        
        // GET api/values
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]LoginModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            var u = _context.User.FirstOrDefault(x => x.Mail == user.UserName && x.Password == user.Password);

            if (u != null)
            {
               

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
 
                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:5000",
                    audience: "http://localhost:5000",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );
 
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString, User = u});
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost, Route("signup")]
        public IActionResult Signup([FromBody]SignupModel user)
        {

            var u = new User()
            {
                Birthday = user.birthday,
                FullName = user.firstName,
                LastName = user.lastName,
                Mail = user.email,
                Password = user.password,
                FacultyId = user.faculty,
                PhoneNumber = user.phone,
                RoleId = 3
                
            };

            _context.User.Add(u);
            _context.SaveChanges();

            return new JsonResult("Success");

        }
    }
}