using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SystemSupportingMSE.Controllers.Resource;
using SystemSupportingMSE.Controllers.Resource.Roles;
using SystemSupportingMSE.Controllers.Resource.Teams;
using SystemSupportingMSE.Controllers.Resource.Users;
using SystemSupportingMSE.Core.Models;

namespace SystemSupportingMSE.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            /*****************************************/
            //Domain To API
            /*****************************************/
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
            CreateMap<Gender, KeyValuePairResource>();
            CreateMap<Role, KeyValuePairResource>();
            CreateMap<Team, KeyValuePairResource>();

            //Events

            //Roles
            CreateMap<Role, RoleResource>();
            CreateMap<User, UserRolesResource>()
                .ForMember(ur => ur.Roles, opt => opt.MapFrom(u => u.Roles.Select(ur => new Role { Id = ur.Role.Id, Name = ur.Role.Name })));

            //Teams
            CreateMap<Team, TeamResource>()
                .ForMember(tr => tr.Users, opt => opt.MapFrom(t => t.Users.Select(ut => new TeamUserData { Id = ut.User.Id, Name = ut.User.Name, Surname = ut.User.Surname, Status = ut.Status})));

            //Users
            CreateMap<User, UserProfileResource>()
                .ForMember(up => up.Roles, opt => opt.MapFrom(u => u.Roles.Select(ur => new Role { Id = ur.Role.Id, Name = ur.Role.Name })))
                .ForMember(up => up.Teams, opt => opt.MapFrom(u => u.Teams.Select(ut => new Team { Id = ut.Team.Id, Name = ut.Team.Name })))
                .ForMember(up => up.Gender, opt => opt.MapFrom(u => u.Gender));

            /*****************************************/
            //API to Domain
            /*****************************************/
            CreateMap<UserQueryResource, UserQuery>();

            //Events

            //Roles
            CreateMap<RoleResource, Role>()
                .ForMember(r => r.Id, opt => opt.Ignore())
                .ForMember(r => r.Name, opt => opt.Ignore());
            CreateMap<UserSaveRolesResource, User>()
               .ForMember(ur => ur.Id, opt => opt.Ignore())
               .ForMember(ur => ur.Roles, opt => opt.Ignore())
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