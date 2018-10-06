using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemSupportingMSE.Controllers.Resource.Competitions;
using SystemSupportingMSE.Core;
using SystemSupportingMSE.Core.Models.Events;

namespace SystemSupportingMSE.Controllers
{
    [Route("/api/competitions")]
    public class CompetitionController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICompetitionRepository competitionRepository;

        public CompetitionController(IMapper mapper, IUnitOfWork unitOfWork, ICompetitionRepository competitionRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.competitionRepository = competitionRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<CompetitionResource>> GetEvents()
        {
            var events = await competitionRepository.GetCompetitions();

            return mapper.Map<IEnumerable<Competition>, IEnumerable<CompetitionResource>>(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var e = await competitionRepository.GetCompetition(id);
            if (e == null)
                return NotFound();

            var result = mapper.Map<Competition, CompetitionResource>(e);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompetition([FromBody] CompetitionSaveResource competitionResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (competitionResource.GroupsRequired == true && competitionResource.GroupSize == null)
                return BadRequest("Group size can not be null if groups are required.");

            // if(competitionResource.TeamRequired == true && competitionResource.TeamSize == null)
            //     return BadRequest("Team size can not be null if team are required.");

            if (competitionResource.GroupsRequired == false && competitionResource.GroupSize != null)
                competitionResource.GroupSize = null;

            var competition = mapper.Map<CompetitionSaveResource, Competition>(competitionResource);

            competitionRepository.Add(competition);
            await unitOfWork.Complete();

            competition = await competitionRepository.GetCompetition(competition.Id);

            var result = mapper.Map<Competition, CompetitionResource>(competition);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCompetition([FromBody] CompetitionSaveResource competitionResource, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (competitionResource.GroupsRequired == true && competitionResource.GroupSize == null)
                return BadRequest("Group size can not be null if groups are required.");

            // if(competitionResource.TeamRequired == true && competitionResource.TeamSize == null)
            //     return BadRequest("Team size can not be null if team are required.");

            var competition = await competitionRepository.GetCompetition(id);
            if (competition == null)
                return BadRequest("Invalid competition id.");

            if (competitionResource.GroupsRequired == false && competitionResource.GroupSize != null)
                competitionResource.GroupSize = null;

            mapper.Map<CompetitionSaveResource, Competition>(competitionResource, competition);
            await unitOfWork.Complete();

            var result = mapper.Map<Competition, CompetitionResource>(competition);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCompetition(int id)
        {
            var competition = await competitionRepository.GetCompetition(id);
            if (competition == null)
                return BadRequest("Invalid id.");

            competitionRepository.Remove(competition);
            await unitOfWork.Complete();

            return Ok();
        }
    }
}