using RubicProg.BusinessLogic.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RubicProg.BusinessLogic.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserInformationBlo> RegistrationUser(UserRegistrBlo userRegistrBlo);
        Task<UserInformationBlo> AuthenticationUser(UserIdentityBlo userIdentityBlo);
        Task<UserInformationBlo> GetUser(int userId);
        Task<UserInformationBlo> UpdateUser(int userId, UserUpdateBlo userUpdateBlo);
        Task<bool> DoesExistUser(int userId);
        Task<bool> DeleteUser(int userId);

        Task<WorkoutInformationBlo> AddWorkoutPlan(int workoutId, WorkoutPlanAddBlo workoutPlanAddBlo);
        Task<WorkoutInformationBlo> UpdateWorkoutPlan(int userId, WorkoutPlanUpdateBlo workoutPlanUpdateBlo);
        Task<WorkoutInformationBlo> GetWorkoutPlan(int workoutPlanId);
        Task<List<WorkoutInformationBlo>> GetAllWorkoutPlans(int userId, int count, int skipCount);
        Task<bool> DoesExistWorkout(int workoutPlanId);
        Task<bool> DeleteWorkoutPlan(int workoutPlanId);
    }
}
