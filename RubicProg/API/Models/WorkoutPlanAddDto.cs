using System;

namespace RubicProg.API.Models
{
    public class WorkoutPlanAddDto
    {
        public int UserWhoTrainingId { get; set; }
        public string Exercise { get; set; }
        public bool IsDone { get; set; }
        public int WorkoutTime { get; set; }
/*        public DateTime StartWorkoutDate { get; set; }*/
    }
}
