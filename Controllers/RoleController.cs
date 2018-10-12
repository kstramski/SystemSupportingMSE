using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemSupportingMSE.Controllers.Resource;
using SystemSupportingMSE.Controllers.Resource.Roles;
using SystemSupportingMSE.Controllers.Resource.Users;
using SystemSupportingMSE.Core;
using SystemSupportingMSE.Core.Models;
using SystemSupportingMSE.Core.Models.Query;

namespace SystemSupportingMSE.Controllers
{
    [Route("/api/roles")]
    public class RoleController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IRoleRepository roleRepository;
        private readonly IUserRepository userRepository;

        public RoleController(IMapper mapper, IUnitOfWork unitOfWork, IRoleRepository roleRepository, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.roleRepository = roleRepository;
            this.userRepository = userRepository;
        }

        [HttpGet]
        [Authorize(Roles="Administrator")]
        public async Task<IEnumerable<RoleResource>> GetRoles()
        {
            var roles = await roleRepository.GetRoles();
            
            return mapper.Map<IEnumerable<Role>, IEnumerable<RoleResource>>(roles);
        }

        [HttpGet("{id}")]
        [Authorize(Roles="Administrator")]
        public async Task<IActionResult> GetRole(int id)
        {
            var role = await roleRepository.GetRole(id);
            if(role == null)
                return NotFound();

            var result = mapper.Map<Role, RoleResource>(role);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles="Administrator")]
        public async Task<IActionResult> EditDescription([FromBody] RoleResource roleResource, int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var role = await roleRepository.GetRole(id);
            if(role == null)
                return NotFound();

            mapper.Map<RoleResource, Role>(roleResource, role);
            await unitOfWork.Complete();

            var result = mapper.Map<Role, RoleResource>(role);

            return Ok(roleResource);
        }

        [HttpGet("users")]
        [Authorize(Roles="Administrator")]
        public async Task<QueryResultResource<UserRolesResource>> GetUsers(UserQueryResource filterResource)
        {
                var filter = mapper.Map<UserQueryResource, UserQuery>(filterResource);
                var queryResult = await userRepository.GetUsers(filter);
                return mapper.Map<QueryResult<User>, QueryResultResource<UserRolesResource>>(queryResult);
        }

        [HttpGet("users/{id}")]
        [Authorize(Roles="Administrator")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await userRepository.GetUser(id);
            if(user == null)
                return NotFound();

            var result = mapper.Map<User, UserRolesResource>(user);

            return Ok(result);
        }

        [HttpPut("users/{id}")]
        [Authorize(Roles="Administrator")]
        public async Task<IActionResult> EditUserRoles([FromBody] UserSaveRolesResource userResource, int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userRepository.GetUser(id);
            if(user == null)
                return NotFound();

            if(await roleRepository.CheckAdmin(userResource, user))
                return BadRequest("You can not remove administrator role because only one administrator exist.");

            mapper.Map<UserSaveRolesResource, User>(userResource, user);
            await unitOfWork.Complete();

            user = await userRepository.GetUser(id);// do poprawy
            var result = mapper.Map<User, UserRolesResource>(user);
            
            return Ok(result);
        }

        [HttpGet("count")]
        [Authorize(Roles="Administrator")]
        public async Task<IActionResult> RoleCount()
        {
            var count = await roleRepository.RoleCount(1);

            return Ok(count);
        }

    }
}