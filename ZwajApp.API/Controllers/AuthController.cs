using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ZwajApp.API.Data;
using ZwajApp.API.Dtos;
using ZwajApp.API.Models;

namespace ZwajApp.API.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;
            
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto){
            userRegisterDto.username = userRegisterDto.username.ToLower();
            if (await _repo.UserExists(userRegisterDto.username)) return BadRequest("اسم المستخدم موجود بالفعل");
            var userToCreate = new User{
                UserName = userRegisterDto.username
            };
            var createdUser = await _repo.Register(userToCreate,userRegisterDto.password);
            return StatusCode(201);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto){
            var UserFromRepo = await _repo.Login(userLoginDto.username.ToLower(),userLoginDto.password);
            if (UserFromRepo == null) return Unauthorized();

            //Claims
            var Claims = new[]{
                new Claim(ClaimTypes.NameIdentifier,UserFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name,UserFromRepo.UserName)
            };

            //Key
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("_config.GetSection('AppSettings : Token').Value"));

            //Credentials
            var Creds = new SigningCredentials(Key,SecurityAlgorithms.HmacSha512);

            //TokenDescriptor
            var TokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(Claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = Creds
            };

            //TokenHandler
            var TokenHandler = new JwtSecurityTokenHandler();

            //Token
            var Token = TokenHandler.CreateToken(TokenDescriptor);

            return Ok(new{Token = TokenHandler.WriteToken(Token)});





        }
    }
}