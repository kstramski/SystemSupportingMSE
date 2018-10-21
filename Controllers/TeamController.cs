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
using SystemSupportingMSE.Core.Models.Query;

namespace SystemSupportingMSE.Controllers
{
    [Route("/api/teams")]
    public class TeamController : Controller
    {
        private readonly IMapper mapper;
        private readonly IAuthRepository authRepository;
        private readonly ITeamRepository teamRepository;
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;

        public TeamController(IMapper mapper, IAuthRepository authRepository, ITeamRepository teamRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.authRepository = authRepository;
            this.teamRepository = teamRepository;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<QueryResult<TeamResource>> GetTeams(TeamQueryResource filterResource)
        {
            var filter = mapper.Map<TeamQueryResource, TeamQuery>(filterResource);
            var queryResult = await teamRepository.GetTeams(filter);

            return mapper.Map<QueryResult<Team>, QueryResult<TeamResource>>(queryResult);

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

        [HttpPut("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> EditTeam([FromBody] TeamSaveResource teamResource, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var team = await teamRepository.GetTeam(id);
            if (team == null)
                return NotFound();

            mapper.Map<TeamSaveResource, Team>(teamResource, team);
            await unitOfWork.Complete();

            //team = await teamRepository.GetTeam(team.Id);

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

            if (!authRepository.IsModerator(User)
            && !authRepository.IsAuthorizedById(User, team.Captain))
                return Unauthorized();

            if (team.Users.Where(ut => ut.Status == true).Count() > 1)
                return BadRequest("You can not remove team with active users.");

            teamRepository.Remove(team);
            await unitOfWork.Complete();

            return Ok();
        }

        [HttpPut("add/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddUserToTeam([FromBody] TeamSaveUserResource userResource, int id)
        {
            var team = await teamRepository.GetTeam(id);
            if (team == null)
                return NotFound();

            if (!authRepository.IsModerator(User)
            && !authRepository.IsAuthorizedById(User, team.Captain))
                return Unauthorized();

            if (string.IsNullOrWhiteSpace(userResource.Email))
                return BadRequest("Email field can not be empty.");

            var user = await userRepository.GetUser(0, userResource.Email);
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
        public async Task<IActionResult> RemoveUserFromTeam([FromBody] int userId, int id)
        {
            if (userId == 0)
                return BadRequest("Invalid UserId.");

            var team = await teamRepository.GetTeam(id);
            if (team == null)
                return NotFound();

            if (!authRepository.IsModerator(User)
            && !authRepository.IsAuthorizedById(User, team.Captain)
            && !authRepository.IsAuthorizedById(User, userId))
                return Unauthorized();

            if (userId == team.Captain)
                return BadRequest("Captain can not be removed.");

            var user = teamRepository.GetUserTeam(team, userId);
            if (user == null)
                return BadRequest("User is not belong to this team.");

            team.Users.Remove(user);
            await unitOfWork.Complete();

            var result = mapper.Map<Team, TeamResource>(team);

            return Ok(result);
        }

        [HttpPut("status/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ChangeUserStatus([FromBody] int userId, int id)
        {
            if (userId == 0)
                return BadRequest("Invalid UserId.");

            var team = await teamRepository.GetTeam(id);
            if (team == null)
                return NotFound();

            if (!authRepository.IsModerator(User)
            && !authRepository.IsAuthorizedById(User, userId))
                return Unauthorized();

            var user = teamRepository.GetUserTeam(team, userId);
            if (user == null)
                return BadRequest("User is not belong to this team.");

            user.Status = true;
            await unitOfWork.Complete();

            var result = mapper.Map<Team, TeamResource>(team);

            return Ok(result);
        }

    }
}