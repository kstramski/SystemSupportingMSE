using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SystemSupportingMSE.Controllers.Resource;
using SystemSupportingMSE.Controllers.Resource.Roles;
using SystemSupportingMSE.Controllers.Resource.Users;
using SystemSupportingMSE.Core.Models;

namespace SystemSupportingMSE.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            //Model to Domain
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
            CreateMap<Role, KeyValuePairResource>();
            CreateMap<Team, KeyValuePairResource>();

            CreateMap<Role, RoleResource>();
            CreateMap<User, UserRolesResource>()
                .ForMember(ur => ur.Roles, opt => opt.MapFrom(u => u.Roles.Select(ur => new Role{ Id = ur.Role.Id, Name = ur.Role.Name })));
            CreateMap<User, UserProfileResource>()
                .ForMember(up => up.Roles, opt => opt.MapFrom(u => u.Roles.Select(ur => new Role{ Id = ur.Role.Id, Name = ur.Role.Name })))
                .ForMember(up => up.Teams, opt => opt.MapFrom(u => u.Teams.Select(ut => new Team{ Id = ut.Team.Id, Name = ut.Team.Name })));


            //Api to Domain
            CreateMap<UserQueryResource, UserQuery>();
            CreateMap<RoleResource, Role>()
                .ForMember(r => r.Id, opt => opt.Ignore())
                .ForMember(r => r.Name, opt => opt.Ignore());
            CreateMap<UserSaveRolesResource, User>()
                .ForMember(ur => ur.Id, opt => opt.Ignore())
                .ForMember(ur => ur.Roles, opt => opt.Ignore())
                .AfterMap((ur, u) => {
                    var removedRoles = u.Roles
                        .Where(r => !ur.Roles
                            .Contains(r.RoleId))
                        .ToList();
                    foreach (var r in removedRoles)
                        u.Roles.Remove(r);

                    var addRoles = ur.Roles
                        .Where(id => !u.Roles
                            .Any(r => r.RoleId == id))
                        .Select(id => new UserRole{ RoleId = id})
                        .ToList();
                    foreach(var r in addRoles)
                        u.Roles.Add(r);
                });

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