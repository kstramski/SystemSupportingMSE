using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SystemSupportingMSE.Controllers.Resource;
using SystemSupportingMSE.Core;
using SystemSupportingMSE.Core.Models;

namespace SystemSupportingMSE.Controllers
{
    [Route("/api/roles")]
    public class RoleController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IRoleRepository roleRepository;

        public RoleController(IMapper mapper, IUnitOfWork unitOfWork, IRoleRepository roleRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.roleRepository = roleRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<KeyValuePairResource>> GetRoles()
        {
            var roles = await roleRepository.GetRoles();
            
            return mapper.Map<IEnumerable<Role>, IEnumerable<KeyValuePairResource>>(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            var role = await roleRepository.GetRole(id);
            if(role == null)
                return NotFound();

            var result = mapper.Map<Role, RoleResource>(role);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDescription([FromBody] RoleResource roleResource, int id)
        {
            var role = await roleRepository.GetRole(id);
            if(role == null)
                return NotFound();

            mapper.Map<RoleResource, Role>(roleResource, role);
            await unitOfWork.Complete();

            var result = mapper.Map<Role, RoleResource>(role);

            return Ok(result);
        }

    }
}