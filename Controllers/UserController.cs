using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SystemSupportingMSE.Controllers.Resource;
using SystemSupportingMSE.Core;
using SystemSupportingMSE.Core.Models;

namespace SystemSupportingMSE.Controllers
{   
    [Route("/api/users")]
    public class UserController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepository;

        public UserController(IMapper mapper, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<UserProfileResource>> GetUsers()
        {
            var users = await userRepository.GetUsers();
            var result = mapper.Map<IEnumerable<User>, IEnumerable<UserProfileResource>>(users);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await userRepository.GetUser(id);
            if(user == null)
                return NotFound();

            var result = mapper.Map<User, UserProfileResource>(user);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserSaveResource userSaveResource)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = mapper.Map<UserSaveResource, User>(userSaveResource);
            user.DateOfRegistration = DateTime.Now;

            userRepository.Add(user, userSaveResource.Password);
            await unitOfWork.Complete();

            user = await userRepository.GetUser(user.Id);
            
            var result = mapper.Map<User, UserProfileResource>(user);

            return Ok(result);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserSaveProfileResource userResource, int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userRepository.GetUser(id);
            if(user == null)
                return NotFound();

            mapper.Map<UserSaveProfileResource, User>(userResource, user);
            await unitOfWork.Complete();    

            var result = mapper.Map<User, UserProfileResource>(user);
            
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await userRepository.GetUser(id);
            if(user == null)
                return NotFound();

            userRepository.Remove(user);
            await unitOfWork.Complete();

            return Ok();
        }

        [HttpPut("{id}/password")]
        public async Task<IActionResult> UpdateUserPassword([FromBody] UserNewPasswordResource userResource, int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(String.IsNullOrWhiteSpace(userResource.Email))
                return BadRequest("Email field can not be empty");

            if(String.IsNullOrWhiteSpace(userResource.Password) 
            || String.IsNullOrWhiteSpace(userResource.NewPassword) 
            || String.IsNullOrWhiteSpace(userResource.NewPasswordRepeat))
                return BadRequest("Password field can not be empty.");

            if(userResource.NewPassword != userResource.NewPasswordRepeat)
                return BadRequest("New password must be equal in two fileds.");

            var user = await userRepository.GetUser(id);
            if(user == null)
                return NotFound();

            if(user.Email != userResource.Email)
                return BadRequest("Invalid email.");
            
            userRepository.SetNewPassword(user, userResource.Password, userResource.NewPassword);
            await unitOfWork.Complete();

            return Ok();
        }

    }
}