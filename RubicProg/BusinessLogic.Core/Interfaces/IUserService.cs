using RubicProg.BusinessLogic.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RubicProg.BusinessLogic.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserUpdateBlo> RegistrationWithEmail(string email, string password);
        Task<UserUpdateBlo> AuthWithEmail(string email, string password);
        Task<UserUpdateBlo> Get(int userId);
        Task<bool> DoesExist(string email, string password);
        Task<UserUpdateBlo> Update(string email, string password, UserUpdateDobleBlo userUpdateDobleBlo);
        Task<UserProfileBlo> UpdateUserProfile(int one ,UserProfileUpdateBlo userProfileUpdateBlo);
        Task<WorkoutPlanBlo> UpdateWorkoutPlanBlo(int two, WorkoutPlanUpdateBlo workoutPlanUpdateBlo);
        Task<UserUpdateBlo> GetThePassword(string phonenumber, string nuberprefix, string email, UserUpdateDobleBlo userUpdateDobleBlo);
    }
}
