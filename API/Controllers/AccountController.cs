
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;



namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        public AccountController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("register")]

        public async Task<ActionResult<AppUser>> Register(RegisterDTO regiterDTO)
        {
            if ( await UserExists(regiterDTO.Username)) return BadRequest("Username is Taken");
          using var hmac = new HMACSHA512();

          var user = new AppUser{
              
              UserName = regiterDTO.Username.ToLower(),
              PasswordHash  =hmac.ComputeHash(Encoding.UTF8.GetBytes(regiterDTO.Password)),
              PasswordSalt =hmac.Key

          };
          _context.Users.Add(user);
          await _context.SaveChangesAsync();

          return user;
          
        
        }

        [HttpPost("login")]

        public async Task<ActionResult<AppUser>> Login(LoginDTO loginDTO)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(x => x.UserName == loginDTO.Username);

                if (user == null) return Unauthorized("InValid username");

             using var hmac = new HMACSHA512(user.PasswordSalt);

             var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

             for ( int i = 0; i< computedHash.Length; i++)
             {
                 if ( computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
             }
             return user;             


        }

        private async Task<bool> UserExists(string username)
        {

            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower() );
            
        }

    }
}