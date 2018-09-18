using AutoMapper;
using SystemSupportingMSE.Controllers.Resource;
using SystemSupportingMSE.Core.Models;

namespace SystemSupportingMSE.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            //Model to Domain
            CreateMap<User, UserProfileResource>();


            //Api to Domain
            CreateMap<UserAuthResource, User>();
            CreateMap<UserProfileResource, User>();
            CreateMap<UserSaveResource, User>();
            CreateMap<UserSaveProfileResource, User>()
                .ForMember(u => u.Id, opt => opt.Ignore());
        }
    }
}