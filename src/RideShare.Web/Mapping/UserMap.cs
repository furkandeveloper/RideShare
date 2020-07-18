using AutoMapper;
using RideShare.Web.Dtos.Request;
using RideShare.Web.Dtos.Response;
using RideShare.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideShare.Web.Mapping
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<User, UserResponseDto>().ReverseMap();

            CreateMap<UserRequestDto, User>().ReverseMap();

            CreateMap<UserUpdateRequestDto,User>().ForAllMembers(opt => opt.Condition((source, destination, member) => member != null));
        }
    }
}
