using RubicProg.BusinessLogic.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RubicProg.BusinessLogic.Core.Interfaces
{
    public interface IWorkoutService
    {
        Task<WorkoutInformationBlo> AddWorkoutPlan(WorkoutPlanAddBlo workoutPlanAddBlo);
/*        Task<WorkoutInformationBlo> UpdateWorkoutPlan(int workoutPlanId, WorkoutPlanUpdateBlo workoutPlanUpdateBlo);*/
        Task<WorkoutInformationBlo> GetWorkoutPlan(int workoutPlanId);
        Task<List<WorkoutInformationBlo>> GetAllWorkoutPlans(int userId, int count, int skipCount);
        Task<int> GetWorkoutCount(int userId);
        Task<bool> DoesExistWorkout(int workoutPlanId);
        Task<bool> DeleteWorkoutPlan(int workoutPlanId);
        Task<bool> DeleteAllWorkoutPlan(int userId);
    }
}
