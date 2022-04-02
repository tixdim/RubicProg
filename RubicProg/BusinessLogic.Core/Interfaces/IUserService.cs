﻿using RubicProg.BusinessLogic.Core.Models;
using System.Threading.Tasks;

namespace RubicProg.BusinessLogic.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserUpdateBlo> RegistrationWithEmail(string email, string password, string nickname);
        Task<UserUpdateBlo> AuthWithEmail(string email, string password);
        Task<UserIdGetBlo> Get(int userId);
        Task<bool> DoesExist(int id);
        Task<UserUpdateBlo> Update(int id, UserUpdateDobleBlo userUpdateDobleBlo);
        Task<UserProfileBlo> UpdateUserProfile(int userWhoProfileId ,UserProfileUpdateBlo userProfileUpdateBlo);
        Task<WorkoutPlanBlo> UpdateWorkoutPlanBlo(int userWhoProfileId, WorkoutPlanUpdateBlo workoutPlanUpdateBlo);
        Task<WorkoutPlanBlo> GetWorkoutPlanBlo();
        Task<WorkoutPlanBlo> AddWorkoutPlanBlo(WorkoutPlanAddBlo workoutPlanAddBlo);
        Task<WorkoutPlanBlo> DeleteWorkoutPlanBlo();
        // всё вроде найс, исправить мапперы и добавить обновление пароля по почте
        // надо сделать добавление юзер профиля и воркают плана
        // также нужно сделать удаление тренировки и удаление юзера целиком
    }
}
