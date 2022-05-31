using System;

namespace RubicProg.BusinessLogic.Core.Models
{
    public class WorkoutPlanAddBlo
    {
        public int UserWhoTrainingId { get; set; }
        public string Exercise { get; set; }
        public bool IsDone { get; set; }
        public DateTime WorkoutTime { get; set; }
        public DateTime StartWorkoutDate { get; set; }
    }
}
