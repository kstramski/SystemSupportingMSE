using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SystemSupportingMSE.Controllers.Resource;
using SystemSupportingMSE.Controllers.Resource.Users;
using SystemSupportingMSE.Core;
using SystemSupportingMSE.Core.Models;
using SystemSupportingMSE.Helpers;

namespace SystemSupportingMSE.Controllers
{
    [Route("/api/users")]
    public class UserController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepository;
        private readonly AuthSettings authSettings;

        public UserController(IMapper mapper, IUnitOfWork unitOfWork, IUserRepository userRepository, IOptions<AuthSettings> authSettings)
        {
            this.authSettings = authSettings.Value;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.userRepository = userRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterResource userRegisterResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = mapper.Map<UserRegisterResource, User>(userRegisterResource);
            user.DateOfRegistration = DateTime.Now;
            user.Roles.Add(new UserRole{RoleId = 3});

            userRepository.Add(user, userRegisterResource.Password);
            await unitOfWork.Complete();

            user = await userRepository.GetUser(user.Id);

            var result = mapper.Map<User, UserProfileResource>(user);

            return Ok(result);
        }

        //Do AuthController /api/auth
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserAuthResource userResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (String.IsNullOrWhiteSpace(userResource.Email) || String.IsNullOrWhiteSpace(userResource.Password))
                return BadRequest();

            var user = await userRepository.AuthenticateUser(userResource.Email, userResource.Password);
            if (user == null)
                return Unauthorized();

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512);
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.Name.ToString()));
            foreach (var role in user.Roles)
                claims.Add(new Claim(ClaimTypes.Role, role.Role.Name));
            
            var tokeOptions = new JwtSecurityToken(
                issuer: authSettings.Domain,
                audience: authSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signinCredentials
            );

            user.LastLogin = DateTime.Now;
            await unitOfWork.Complete();

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return Ok(new { Token = tokenString });
        }

        [HttpGet]
        [Authorize(Roles="Moderator")]
        public async Task<QueryResultResource<UserProfileResource>> GetUsers(UserQueryResource filterResource)
        {
            var filter = mapper.Map<UserQueryResource, UserQuery>(filterResource); 
            var queryResult = await userRepository.GetUsers(filter);
            
            return  mapper.Map<QueryResult<User>, QueryResultResource<UserProfileResource>>(queryResult);
        }

        [HttpGet("{id}")]
        [Authorize(Roles="User")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await userRepository.GetUser(id);
            if (user == null)
                return NotFound();

            if(!User.Claims.Where(c => c.Type == ClaimTypes.Role).Where(r => r.Value == "Moderator").Any()
            && User.FindFirst(ClaimTypes.NameIdentifier).Value != id.ToString())
                return Unauthorized();
            

            var result = mapper.Map<User, UserProfileResource>(user);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles="User")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserSaveProfileResource userResource, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userRepository.GetUser(id);
            if (user == null)
                return NotFound();
            
            if(!User.Claims.Where(c => c.Type == ClaimTypes.Role).Where(r => r.Value == "Moderator").Any())
                if(User.FindFirst(ClaimTypes.NameIdentifier).Value != id.ToString())
                    return Unauthorized();

            mapper.Map<UserSaveProfileResource, User>(userResource, user);
            await unitOfWork.Complete();

            var result = mapper.Map<User, UserProfileResource>(user);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles="Administrator")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await userRepository.GetUser(id);
            if (user == null)
                return NotFound();

            userRepository.Remove(user);
            await unitOfWork.Complete();

            return Ok();
        }

        [HttpPut("changeEmail/{id}")]
        [Authorize(Roles="User")]
        public async Task<IActionResult> ChangeUserEmail([FromBody] UserNewEmailResource userResource, int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(String.IsNullOrWhiteSpace(userResource.Email)
            || String.IsNullOrWhiteSpace(userResource.NewEmail))
                return BadRequest("Email fields can not be empty.");

            var user = await userRepository.GetUser(id);
            if(user == null)
                return NotFound();

            if(User.FindFirst(ClaimTypes.NameIdentifier).Value != id.ToString())
                return Unauthorized();

            if(userResource.Email != user.Email)
                return BadRequest("Invalid email.");

            mapper.Map<UserNewEmailResource, User>(userResource, user);
            await unitOfWork.Complete();

            return Ok();
        }

        [HttpPut("changePassword/{id}")]
        [Authorize(Roles="User")]
        public async Task<IActionResult> ChangeUserPassword([FromBody] UserNewPasswordResource userResource, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (String.IsNullOrWhiteSpace(userResource.Email))
                return BadRequest("Email field can not be empty.");

            if (String.IsNullOrWhiteSpace(userResource.Password)
            || String.IsNullOrWhiteSpace(userResource.NewPassword)
            || String.IsNullOrWhiteSpace(userResource.NewPasswordRepeat))
                return BadRequest("Password fields can not be empty.");

            if (userResource.NewPassword != userResource.NewPasswordRepeat)
                return BadRequest("New password must be equal in two fileds.");

            var user = await userRepository.GetUser(id);
            if (user == null)
                return NotFound();

            if(User.FindFirst(ClaimTypes.NameIdentifier).Value != id.ToString())
                return Unauthorized();

            if (user.Email != userResource.Email)
                return BadRequest("Invalid email.");

            userRepository.SetNewPassword(user, userResource.Password, userResource.NewPassword);
            await unitOfWork.Complete();

            return Ok();
        }

    }
}