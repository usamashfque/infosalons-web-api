using Infosalons.Domain.Interfaces;
using Infosalons.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infosalons.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public UserController(IUnitOfWork unitOfWork, IUserRepository userRepository, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _config = config;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _unitOfWork.Users.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<User> Get(int id)
        {
            return await _unitOfWork.Users.Get(id);
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> SignUpAsync(User user)
        {
            var _user = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                DateAdded = DateTime.Now,
                DateUpdated = DateTime.Now
            };

            var result = await _unitOfWork.Users.Add(_user);
            _unitOfWork.Complete();
            if (result is not null)
            {
                return Ok("User Created");
            }
            else
            {
                return BadRequest("Error in User the Product");
            }
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> SignInAsync(SignIn cred)
        {
            var result = await _userRepository.SignIn(cred);
            if (result is not null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("JWT:Secret")));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: _config.GetValue<string>("JWT:ValidIssuer"),
                    audience: _config.GetValue<string>("JWT:ValidAudience"),
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);


                return Ok(new UserViewModel
                {
                    Id = result.Id,
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    Email = result.Email,
                    Token = tokenString,
                    DateAdded = result.DateAdded,
                    DateUpdated = result.DateUpdated
                });

                //return Ok("User Login Sucessfully");
            }
            else
            {
                return BadRequest("User Login failed");
            }
        }

        [HttpPut]
        public IActionResult UpdateProduct(User user)
        {
            _unitOfWork.Users.Update(user);
            _unitOfWork.Complete();
            return Ok("User Updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var _user = await _unitOfWork.Users.Get(id);

            _unitOfWork.Users.Delete(_user);
            _unitOfWork.Complete();
            return Ok("User Updated");
        }
    }
}
