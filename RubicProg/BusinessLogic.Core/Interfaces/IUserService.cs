using RubicProg.BusinessLogic.Core.Models;
using System.Threading.Tasks;

namespace RubicProg.BusinessLogic.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserUpdateBlo> RegistrationWithEmail(string email, string password, string nickname);
        Task<UserUpdateBlo> AuthWithEmail(string email, string password);
        Task<UserIdGetBlo> Get(int userId);
        Task<UserUpdateBlo> Update(int id, UserUpdateDobleBlo userUpdateDobleBlo);
        Task<bool> DoesExist(int id);
        Task<UserProfileBlo> UpdateUserProfile(int userWhoProfileId ,UserProfileUpdateBlo userProfileUpdateBlo);
        Task<UserProfileBlo> GetUserProfile(int userWhoProfileId);
        Task<UserProfileBlo> AddUserProfile(UserProfileDoubleBlo userProfileAddBlo);
        Task<bool> DeleteUserProfile(int userWhoProfileId);
        Task<WorkoutPlanBlo> UpdateWorkoutPlan(int userWhoProfileId, WorkoutPlanUpdateBlo workoutPlanUpdateBlo);
        Task<WorkoutPlanBlo> GetWorkoutPlan(int workoutPlanId);
        Task<WorkoutPlanBlo> AddWorkoutPlan(WorkoutPlanAddBlo workoutPlanAddBlo);
        Task<bool> DeleteWorkoutPlanBlo(int workoutPlanId);
        // всё вроде найс, добавить обновление пароля по почте
    }
}
