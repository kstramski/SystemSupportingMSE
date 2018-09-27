using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemSupportingMSE.Controllers.Resource.Teams;
using SystemSupportingMSE.Core;
using SystemSupportingMSE.Core.Models;

namespace SystemSupportingMSE.Controllers
{
    [Route("/api/teams")]
    public class TeamController : Controller
    {
        private readonly IMapper mapper;
        private readonly ITeamRepository teamRepository;
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;

        public TeamController(IMapper mapper, ITeamRepository teamRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.teamRepository = teamRepository;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IEnumerable<TeamResource>> GetTeams()
        {
            var teams = await teamRepository.GetTeams();

            return mapper.Map<IEnumerable<Team>, IEnumerable<TeamResource>>(teams);

        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetTeam(int id)
        {
            var team = await teamRepository.GetTeam(id);
            if (team == null)
                return NotFound();

            var result = mapper.Map<Team, TeamResource>(team);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateTeam([FromBody] TeamSaveResource teamResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var team = mapper.Map<TeamSaveResource, Team>(teamResource);
            team.Users.Add(new UserTeam { UserId = teamResource.Captain, Status = true }); //DateJoined = DateTime.Now

            teamRepository.Add(team);
            await unitOfWork.Complete();

            team = await teamRepository.GetTeam(team.Id);

            var result = mapper.Map<Team, TeamResource>(team);

            return Ok(result);
        }

        [HttpPut("add/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddUserToTeam([FromBody] TeamSaveUserResource teamResource, int id)
        {
            var team = await teamRepository.GetTeam(id);
            if (team == null)
                return NotFound();

            if (!User.Claims.Where(c => c.Type == ClaimTypes.Role).Where(r => r.Value == "Moderator").Any()
            && User.FindFirst(ClaimTypes.NameIdentifier).Value != team.Captain.ToString())
                return Unauthorized();

            if (string.IsNullOrWhiteSpace(teamResource.Email))
                return BadRequest("Email field can not be empty.");

            var user = await userRepository.GetUser(0, teamResource.Email);
            if (user == null)
                return BadRequest("Invalid email.");

            if (teamRepository.GetUserTeam(team, user.Id) != null)
                return BadRequest("User is already in this team");

            team.Users.Add(new UserTeam { UserId = user.Id, Status = false });
            await unitOfWork.Complete();

            var result = mapper.Map<Team, TeamResource>(team);

            return Ok(result);
        }

        [HttpPut("remove/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> RemoveUserFromTeam([FromBody] TeamUserIdResource teamResource, int id)
        {
            if (teamResource.UserId == 0)
                return BadRequest("Invalid UserId.");

            var team = await teamRepository.GetTeam(id);
            if (team == null)
                return NotFound();

            if (!User.Claims.Where(c => c.Type == ClaimTypes.Role).Where(r => r.Value == "Moderator").Any()
            && User.FindFirst(ClaimTypes.NameIdentifier).Value != team.Captain.ToString()
            && User.FindFirst(ClaimTypes.NameIdentifier).Value != teamResource.UserId.ToString())
                return Unauthorized();

            if (teamResource.UserId == team.Captain)
                return BadRequest("Captain can not be removed.");

            var user = teamRepository.GetUserTeam(team, teamResource.UserId);
            if (user == null)
                return BadRequest("User is not belong to this team.");

            team.Users.Remove(user);
            await unitOfWork.Complete();

            var result = mapper.Map<Team, TeamResource>(team);

            return Ok(result);
        }

        [HttpPut("status/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ChangeUserStatus([FromBody] TeamUserIdResource teamResource, int id)
        {
            if (teamResource.UserId == 0)
                return BadRequest("Invalid UserId.");

            var team = await teamRepository.GetTeam(id);
            if (team == null)
                return NotFound();

            if (!User.Claims.Where(c => c.Type == ClaimTypes.Role).Where(r => r.Value == "Moderator").Any()
            && User.FindFirst(ClaimTypes.NameIdentifier).Value != teamResource.UserId.ToString())
                return Unauthorized();

            var user = teamRepository.GetUserTeam(team, teamResource.UserId);
            if (user == null)
                return BadRequest("User is not belong to this team.");

            user.Status = true;
            await unitOfWork.Complete();

            var result = mapper.Map<Team, TeamResource>(team);

            return Ok(result);
        }



        [HttpDelete("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> RemoveTeam(int id)
        {
            var team = await teamRepository.GetTeam(id);
            if (team == null)
                return NotFound();

            if (!User.Claims.Where(c => c.Type == ClaimTypes.Role).Where(r => r.Value == "Moderator").Any()
            && User.FindFirst(ClaimTypes.NameIdentifier).Value != team.Captain.ToString())
                return Unauthorized();

            if (team.Users.Where(ut => ut.Status == true).Count() > 1)
                return BadRequest("You can not remove team with active users.");

            teamRepository.Remove(team);
            await unitOfWork.Complete();

            return Ok();
        }
    }
}