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
    public class TravelMap : Profile
    {
        public TravelMap()
        {
            CreateMap<Travel, TravelResponseDto>().ReverseMap();

            CreateMap<TravelRequestDto, Travel>().ReverseMap();

            CreateMap<TravelUpdateRequestDto,Travel>().ForAllMembers(opt => opt.Condition((source, destination, member) => member != null));
        }
    }
}
