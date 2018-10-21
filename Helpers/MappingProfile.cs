using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SystemSupportingMSE.Controllers.Resource;
using SystemSupportingMSE.Controllers.Resource.Competitions;
using SystemSupportingMSE.Controllers.Resource.Events;
using SystemSupportingMSE.Controllers.Resource.Roles;
using SystemSupportingMSE.Controllers.Resource.Teams;
using SystemSupportingMSE.Controllers.Resource.Users;
using SystemSupportingMSE.Controllers.Resource.UsersCompetitions;
using SystemSupportingMSE.Core.Models;
using SystemSupportingMSE.Core.Models.Events;
using SystemSupportingMSE.Core.Models.Query;

namespace SystemSupportingMSE.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            /*****************************************/
            //Domain To API Resource
            /*****************************************/
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));

            //Competitions
            CreateMap<Competition, CompetitionResource>()
                .ForMember(cr => cr.Events, opt => opt.MapFrom(c => c.Events.Select(ec => new Event { Id = ec.Event.Id, Name = ec.Event.Name })));

            //Events
            CreateMap<Event, EventResource>()
                .ForMember(er => er.Competitions, opt => opt.MapFrom(e => e.Competitions.Select(ec => new Competition { Id = ec.Competition.Id, Name = ec.Competition.Name })));
            CreateMap<Event, EventUsersResource>()
                .ForMember(er => er.Competitions, opt => opt.MapFrom(e => e.Competitions
                    .Select(ec => new CompetitionUsersResource
                    {
                        Id = ec.Competition.Id,
                        Name = ec.Competition.Name,
                        UsersCount = ec.UsersCompetitions
                            .Where(uc => uc.CompetitionId == ec.CompetitionId && uc.EventId == ec.EventId && uc.StageId == 1)
                            .Count()
                    })));

            //EventCompetition
            CreateMap<EventCompetition, EventCompetitionResource>()
                .ForMember(er => er.Event, opt => opt.MapFrom(ec => new Event { Id = ec.Event.Id, Name = ec.Event.Name }))
                .ForMember(er => er.Competition, opt => opt.MapFrom(ec => new Competition { Id = ec.Competition.Id, Name = ec.Competition.Name }))
                .ForMember(er => er.GroupRequired, opt => opt.MapFrom(ec => ec.Competition.GroupsRequired))
                .ForMember(er => er.UsersCount, opt => opt.MapFrom(ec => ec.UsersCompetitions.Where(uc => uc.StageId == 1).Count()));

            //UserCompetition
            CreateMap<UserCompetition, UserCompetitionResource>()
                .ForMember(ur => ur.User, opt => opt.MapFrom(uc => new UserBasicsResource { Id = uc.User.Id, Name = uc.User.Name, Surname = uc.User.Surname }));

            //Roles
            CreateMap<Role, RoleResource>();
            CreateMap<User, UserRolesResource>()
                .ForMember(ur => ur.Roles, opt => opt.MapFrom(u => u.Roles.Select(ur => new Role { Id = ur.Role.Id, Name = ur.Role.Name })));

            //Teams
            CreateMap<Team, TeamResource>()
                .ForMember(tr => tr.Users, opt => opt.MapFrom(t => t.Users.Select(ut => new TeamUserData { Id = ut.User.Id, Name = ut.User.Name, Surname = ut.User.Surname, Status = ut.Status })));

            //Users
            CreateMap<User, UserProfileResource>()
                .ForMember(up => up.Roles, opt => opt.MapFrom(u => u.Roles.Select(ur => new Role { Id = ur.Role.Id, Name = ur.Role.Name })))
                .ForMember(up => up.Teams, opt => opt.MapFrom(u => u.Teams.Select(ut => new Team { Id = ut.Team.Id, Name = ut.Team.Name })))
                .ForMember(up => up.Gender, opt => opt.MapFrom(u => u.Gender));

            /*****************************************/
            //API Resource to Domain
            /*****************************************/
            CreateMap<UserQueryResource, UserQuery>();

            //Competitions
            CreateMap<CompetitionSaveResource, Competition>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            //Events
            CreateMap<EventSaveResource, Event>()
                .ForMember(e => e.Id, opt => opt.Ignore())
                .ForMember(e => e.Competitions, opt => opt.Ignore())
                .AfterMap((esr, e) =>
                {
                    var removeCompetitions = e.Competitions.Where(ec => !esr.Competitions.Contains(ec.CompetitionId)).ToList();
                    foreach (var c in removeCompetitions)
                        e.Competitions.Remove(c);

                    var addCompetitions = esr.Competitions.Where(id => !e.Competitions.Any(ec => ec.CompetitionId == id)).Select(id => new EventCompetition { CompetitionId = id }).ToList();
                    foreach (var c in addCompetitions)
                        e.Competitions.Add(c);
                });

            //EventCompetition 
            CreateMap<EventCompetitionSaveResource, EventCompetition>()
                .ForMember(ec => ec.CompetitionId, opt => opt.Ignore())
                .ForMember(ec => ec.EventId, opt => opt.Ignore());


            //Roles
            CreateMap<RoleResource, Role>()
                .ForMember(r => r.Id, opt => opt.Ignore())
                .ForMember(r => r.Name, opt => opt.Ignore());
            CreateMap<UserSaveRolesResource, User>()
               .ForMember(u => u.Id, opt => opt.Ignore())
               .ForMember(u => u.Roles, opt => opt.Ignore())
               .AfterMap((usr, u) =>
               {
                   var removedRoles = u.Roles
                       .Where(ur => !usr.Roles
                           .Contains(ur.RoleId))
                       .ToList();
                   foreach (var r in removedRoles)
                       u.Roles.Remove(r);

                   var addRoles = usr.Roles
                       .Where(id => !u.Roles
                           .Any(ur => ur.RoleId == id))
                       .Select(id => new UserRole { RoleId = id })
                       .ToList();
                   foreach (var r in addRoles)
                       u.Roles.Add(r);
               });

            //Teams
            CreateMap<TeamSaveResource, Team>()
                .ForMember(t => t.Id, opt => opt.Ignore());

            //Users    
            CreateMap<UserAuthResource, User>();
            CreateMap<UserNewEmailResource, User>()
                .ForMember(u => u.Id, opt => opt.Ignore());
            CreateMap<UserProfileResource, User>();
            CreateMap<UserRegisterResource, User>();
            CreateMap<UserSaveProfileResource, User>()
                .ForMember(u => u.Id, opt => opt.Ignore());
        }
    }
}