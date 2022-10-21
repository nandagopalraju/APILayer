using DataLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ModelLayer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _config;
        public UserController(DataContext dataContext, IConfiguration configuration)
        {
            _dataContext=dataContext;
            _config=configuration;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            if(_dataContext.Users.Any(u =>u.UserName==request.UserName))
            {
                return BadRequest("User already exist");
            }

            CreatePasswordHash(request.Password
                , out byte[] passwordHash
                , out byte[] passwordSalt);

            var user = new User
            {
                UserName = request.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };

            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();

            return Ok("user Succesfully created");
    
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {

            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);
            if(user==null)
            {
                return BadRequest("User not found");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Password is Incorrect");
            }

            string token = CreateToken(user);

            return Ok(token);


        }

        private string CreateToken(User user)
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _config.GetSection("AppSetting:Token").Value));
            
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);
            
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }



        //[HttpPost("verify")]
        //public async Task<IActionResult> Verify(string token)
        //{
        //    var user = await _dataContext.Users.FirstOrDefaultAsync((u => u.VerificationToken == token));
        //    if (user==null)
        //    {
        //        return BadRequest("invalid token");
        //    }

        //    user.VerifiedAt = DateTime.Now;
        //    await _dataContext.SaveChangesAsync();

        //    return Ok("User verified");
                     
        //}





        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.
                    ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
             }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.
                    ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);

            }
        }



        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

    }
}
