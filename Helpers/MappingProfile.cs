using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SystemSupportingMSE.Controllers.Resource;
using SystemSupportingMSE.Core.Models;

namespace SystemSupportingMSE.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            //Model to Domain
            CreateMap<Role, KeyValuePairResource>();
            CreateMap<Role, RoleResource>();
            CreateMap<Team, KeyValuePairResource>();
            CreateMap<User, UserProfileResource>()
                .ForMember(up => up.Roles, opt => opt.MapFrom(u => u.Roles.Select(ur => new Role{ Id = ur.Role.Id, Name = ur.Role.Name })))
                .ForMember(up => up.Teams, opt => opt.MapFrom(u => u.Teams.Select(ut => new Team{ Id = ut.Team.Id, Name = ut.Team.Name })));


            //Api to Domain
            CreateMap<RoleResource, Role>()
                .ForMember(r => r.Id, opt => opt.Ignore())
                .ForMember(r => r.Name, opt => opt.Ignore());
            CreateMap<UserAuthResource, User>();
            CreateMap<UserProfileResource, User>();
            CreateMap<UserRegisterResource, User>();
            CreateMap<UserSaveProfileResource, User>()
                .ForMember(u => u.Id, opt => opt.Ignore());
        }
    }
}