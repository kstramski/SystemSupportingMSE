using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemSupportingMSE.Controllers.Resource;
using SystemSupportingMSE.Controllers.Resource.Events;
using SystemSupportingMSE.Core;
using SystemSupportingMSE.Core.Models;
using SystemSupportingMSE.Core.Models.Events;
using SystemSupportingMSE.Core.Models.Query;

namespace SystemSupportingMSE.Controllers
{
    [Route("/api/events")]
    public class EventController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IEventRepository eventRepository;
        private readonly IAuthRepository authRepository;

        public EventController(IMapper mapper, IUnitOfWork unitOfWork, IEventRepository eventRepository, IAuthRepository authRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.eventRepository = eventRepository;
            this.authRepository = authRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<QueryResultResource<EventResource>> GetEvents(EventQueryResource filterResource)
        {
            var filter = mapper.Map<EventQueryResource, EventQuery>(filterResource);
            var queryResult = await eventRepository.GetEvents(filter);

            return mapper.Map<QueryResult<Event>, QueryResultResource<EventResource>>(queryResult);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEvent(int id)
        {
            var e = await eventRepository.GetEvent(id);
            if (e == null)
                return NotFound();

            var result = mapper.Map<Event, EventUsersResource>(e);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> CreateEvent([FromBody] EventSaveResource eventResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var e = mapper.Map<EventSaveResource, Event>(eventResource);

            eventRepository.Add(e);
            eventRepository.AddDatesToCompetitions(e);
            await unitOfWork.Complete();

            e = await eventRepository.GetEvent(e.Id);

            var result = mapper.Map<Event, EventResource>(e);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> EditEvent([FromBody] EventSaveResource eventResource, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var e = await eventRepository.GetEvent(id);
            if (e == null)
                return NotFound();

            mapper.Map<EventSaveResource, Event>(eventResource, e);
            eventRepository.AddDatesToCompetitions(e);
            await unitOfWork.Complete();

            e = await eventRepository.GetEvent(e.Id);

            var result = mapper.Map<Event, EventResource>(e);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> RemoveEvent(int id)
        {
            var e = await eventRepository.GetEvent(id);
            if (e == null)
                return NotFound();

            eventRepository.Remove(e);
            await unitOfWork.Complete();

            return Ok();
        }

        //Event Competition information
        [HttpGet("{eventId}/competitions/{competitionId}")]
        public async Task<IActionResult> GetEventCompetition(int eventId, int competitionId)
        {
            var eventCompetition = await eventRepository.GetEventCompetition(eventId, competitionId);
            if (eventCompetition == null)
                return NotFound();

            var result = mapper.Map<EventCompetition, EventCompetitionResource>(eventCompetition);

            return Ok(result);
        }

        //Event Competition information
        [HttpPut("{eventId}/competitions/{competitionId}")]
        public async Task<IActionResult> EditEventCompetition([FromBody] EventCompetitionSaveResource eventResource, int eventId, int competitionId)
        {
            var eventCompetition = await eventRepository.GetEventCompetition(eventId, competitionId);
            if (eventCompetition == null)
                return NotFound();

            mapper.Map<EventCompetitionSaveResource, EventCompetition>(eventResource, eventCompetition);
            await unitOfWork.Complete();

            var result = mapper.Map<EventCompetition, EventCompetitionResource>(eventCompetition);

            return Ok(result);
        }

        //Event Competition Users
        [HttpGet("{eventId}/competitions/{competitionId}/participants")]
        [AllowAnonymous]
        public async Task<IEnumerable<UserCompetitionResource>> GetEventCompetitionUsers(int eventId, int competitionId)
        {
            var users = await eventRepository.GetEventCompetitionUsers(eventId, competitionId);

            var result = mapper.Map<IEnumerable<UserCompetition>, IEnumerable<UserCompetitionResource>>(users);

            return result;
        }


        // Assign user to event competition
        // UserCompetitionSaveResource - UserId, CompetitionID | id - EventId
        [HttpPost("{id}/users")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AssignUserToEvent([FromBody] UserCompetitionSaveResource eventResource, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var e = await eventRepository.GetEvent(id);
            if (e == null)
                return NotFound();

            if (!eventRepository.CompetitionExist(e, eventResource.CompetitionId))
                return BadRequest("Invalid competition ID.");

            if (!authRepository.IsAuthorizedById(User, eventResource.UserId) && !authRepository.IsModerator(User))
                return Unauthorized();

            eventRepository.AddUserToCompetition(eventResource.UserId, id, eventResource.CompetitionId);
            await unitOfWork.Complete();

            e = await eventRepository.GetEvent(e.Id);

            var result = mapper.Map<Event, EventResource>(e);

            return Ok(result);
        }

        // Remove user from event competition
        // UserCompetitionSaveResource - UserId, CompetitionID | id - EventId
        [HttpDelete("{id}/users")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> RemoveUserFromEvent([FromBody] UserCompetitionSaveResource eventResource, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var uc = await eventRepository.FindUserCompetition(eventResource.UserId, id, eventResource.CompetitionId);
            if (uc == null)
                return NotFound();

            if (!authRepository.IsAuthorizedById(User, eventResource.UserId) && !authRepository.IsModerator(User))
                return Unauthorized();

            eventRepository.RemoveUserFromEvent(uc);
            await unitOfWork.Complete();

            return Ok();
        }

    }
}