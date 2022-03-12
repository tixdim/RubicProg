using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RubicProg.BusinessLogic.Core.Models;
using RubicProg.DataAccess.Core.Models;

namespace RubicProg.BusinessLogic.AutoMapperProfile
{
    public class BusinessLogicProfile: Profile
    {
        public BusinessLogicProfile()
        {
            CreateMap<UserRto, UserUpdateBlo>()
            .ForMember(x => x.Id, x => x.MapFrom(m => m.Id))
            .ForMember(x => x.Email, x => x.MapFrom(m => m.Email))
            .ForMember(x => x.Password, x => x.MapFrom(m => m.Password))
            .ForMember(x => x.NickName, x => x.MapFrom(m => m.NickName)); 
            CreateMap<UserRto, UserUpdateDobleBlo>()
            .ForMember(x=> x.Id, x => x.MapFrom(m => m.Id))
            .ForMember(x => x.Email, x => x.MapFrom(m => m.Email))
            .ForMember(x => x.Password, x => x.MapFrom(m => m.Password))
            .ForMember(x => x.NickName, x => x.MapFrom(m => m.NickName));
           
        }
    }
}
