using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemSupportingMSE.Controllers.Resource.Events;
using SystemSupportingMSE.Core;
using SystemSupportingMSE.Core.Models.Events;

namespace SystemSupportingMSE.Controllers
{
    [Route("/api/events")]
    public class EventController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IEventRepository eventRepository;

        public EventController(IMapper mapper, IUnitOfWork unitOfWork, IEventRepository eventRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.eventRepository = eventRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<EventResource>> GetEvents()
        {
            var events = await eventRepository.GetEvents();

            return mapper.Map<IEnumerable<Event>, IEnumerable<EventResource>>(events);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEvent(int id)
        {
            var e = await eventRepository.GetEvent(id);
            if(e == null)
                return NotFound();

            var result = mapper.Map<Event, EventResource>(e);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventSaveResource eventResource)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var e = mapper.Map<EventSaveResource, Event>(eventResource);

            eventRepository.Add(e);
            await unitOfWork.Complete();

            e = await eventRepository.GetEvent(e.Id);

            var result = mapper.Map<Event, EventResource>(e);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditEvent([FromBody] EventSaveResource eventResource, int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var e = await eventRepository.GetEvent(id);
            if(e == null)
                return BadRequest("Invalid id.");

            mapper.Map<EventSaveResource, Event>(eventResource, e);
            await unitOfWork.Complete();

            var result = mapper.Map<Event, EventResource>(e);

            return Ok(result);
        }
    }
}