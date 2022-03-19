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
            CreateMap<WorkoutRto, WorkoutPlanBlo>()
                .ForMember(x => x.Id, x => x.MapFrom(m => m.Id))
                .ForMember(x => x.WorkoutTime, x => x.MapFrom(m => m.WorkoutTime))
                .ForMember(x => x.Exercise, x => x.MapFrom(m => m.Exercise));
            CreateMap<UserRto, UserUpdateBlo>()
                .ForMember(x => x.Id, x => x.MapFrom(m => m.Id))
                .ForMember(x => x.Email, x => x.MapFrom(m => m.Email))
                .ForMember(x => x.Password, x => x.MapFrom(m => m.Password))
                .ForMember(x => x.NickName, x => x.MapFrom(m => m.NickName));
            CreateMap<UserRto, UserIdGetBlo>()
                .ForMember(x => x.Id, x => x.MapFrom(m => m.Id))
                .ForMember(x => x.Email, x => x.MapFrom(m => m.Email))
                .ForMember(x => x.NickName, x => x.MapFrom(m => m.NickName));
            CreateMap<ProfileUserRto, UserProfileBlo>()
                .ForMember(x => x.Id, x => x.MapFrom(m => m.Id))
                .ForMember(x => x.IsBoy, x => x.MapFrom(m => m.IsBoy))
                .ForMember(x => x.Name, x => x.MapFrom(m => m.Name))
                .ForMember(x => x.Surname, x => x.MapFrom(m => m.Surname))
                .ForMember(x => x.Birthday, x => x.MapFrom(m => m.Birthday))
                .ForMember(x => x.AvatarUrl, x => x.MapFrom(m => m.AvatarUrl));

        }
    }
}
