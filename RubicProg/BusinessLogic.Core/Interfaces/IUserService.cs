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
        Task<UserProfileBlo> UpdateUserProfile(int userWhoProfileId, UserProfileUpdateBlo userProfileUpdateBlo);
        Task<UserProfileBlo> GetUserProfile(int userWhoProfileId); // контр
        Task<UserProfileBlo> AddUserProfile(UserProfileDoubleBlo userProfileAddBlo); // контр
        Task<bool> DeleteUserProfile(int userWhoProfileId); // контр
        Task<WorkoutPlanBlo> UpdateWorkoutPlan(int userWhoProfileId, WorkoutPlanUpdateBlo workoutPlanUpdateBlo);
        Task<WorkoutPlanBlo> GetWorkoutPlan(int workoutPlanId); // контр
        Task<WorkoutPlanBlo> AddWorkoutPlan(WorkoutPlanAddBlo workoutPlanAddBlo); // контр
        Task<bool> DeleteWorkoutPlanBlo(int workoutPlanId); // контр
        // всё вроде найс, добавить обновление пароля по почте
    }
}
