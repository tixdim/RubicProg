using AutoMapper;
using RubicProg.API.Models;
using RubicProg.BusinessLogic.Core.Models;
using RubicProg.DataAccess.Core.Models;

namespace RubicProg.BusinessLogic.AutoMapperProfile
{
    public class BusinessLogicProfile: Profile
    {
        public BusinessLogicProfile()
        {
            CreateMap<WorkoutRto, WorkoutInformationBlo>()
                .ForMember(x => x.Id, x => x.MapFrom(m => m.Id))
                .ForMember(x => x.UserWhoTraining, x => x.MapFrom(m => m.UserWhoTraining))
                .ForMember(x => x.Exercise, x => x.MapFrom(m => m.Exercise))
                .ForMember(x => x.WorkoutTime, x => x.MapFrom(m => m.WorkoutTime))
                .ForMember(x => x.IsDone, x => x.MapFrom(m => m.IsDone))
                .ForMember(x => x.StartWorkoutDate, x => x.MapFrom(m => m.StartWorkoutDate));

            CreateMap<UserRto, UserInformationBlo>()
                .ForMember(x => x.Id, x => x.MapFrom(m => m.Id))
                .ForMember(x => x.Email, x => x.MapFrom(m => m.Email))
                .ForMember(x => x.Nickname, x => x.MapFrom(m => m.Nickname))
                .ForMember(x => x.IsBoy, x => x.MapFrom(m => m.IsBoy))
                .ForMember(x => x.Name, x => x.MapFrom(m => m.Name))
                .ForMember(x => x.Surname, x => x.MapFrom(m => m.Surname))
                .ForMember(x => x.DateRegistration, x => x.MapFrom(m => m.DateRegistration))
                .ForMember(x => x.AvatarUrl, x => x.MapFrom(m => m.AvatarUrl));

            CreateMap<UserRegistrDto, UserRegistrBlo>();

            CreateMap<UserIdentityDto, UserIdentityBlo>();

            CreateMap<UserUpdateDto, UserUpdateBlo>();

            CreateMap<UserUpdateWithOldPasswordDto, UserUpdateWithOldPasswordBlo>();

            CreateMap<UserUpdateWithNewPasswordDto, UserUpdateWithNewPasswordBlo>();

            CreateMap<UserInformationBlo, UserInformationDto>();

            CreateMap<WorkoutPlanAddDto, WorkoutPlanAddBlo>();

            CreateMap<WorkoutPlanUpdateDto, WorkoutPlanUpdateBlo>();

            CreateMap<WorkoutInformationBlo, WorkoutInformationDto>()
                .ForMember(x => x.Id, x => x.MapFrom(m => m.Id))
                .ForMember(x => x.UserWhoTraining, x => x.MapFrom(m => m.UserWhoTraining))
                .ForMember(x => x.Exercise, x => x.MapFrom(m => m.Exercise))
                .ForMember(x => x.IsDone, x => x.MapFrom(m => m.IsDone))
                .ForMember(x => x.WorkoutTime, x => x.MapFrom(m => m.WorkoutTime))
                .ForMember(x => x.StartWorkoutDate, x => x.MapFrom(m => m.StartWorkoutDate));
        }
    }
}
