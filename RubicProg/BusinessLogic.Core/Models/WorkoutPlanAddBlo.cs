using System;

namespace RubicProg.BusinessLogic.Core.Models
{
    public class WorkoutPlanAddBlo
    {
        public int Id { get; set; }
        public string Exercise { get; set; }
        public int WorkoutTime { get; set; }
        public bool IsDone { get; set; }
        public DateTimeOffset StartWorkoutDate { get; set; }
    }
}
