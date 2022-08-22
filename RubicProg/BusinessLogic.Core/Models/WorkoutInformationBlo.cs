using System;

namespace RubicProg.BusinessLogic.Core.Models
{
    public class WorkoutInformationBlo
    {
        public int Id { get; set; }
        // public UserInformationBlo UserWhoTraining { get; set; }
        public string Exercise { get; set; }
        public bool IsDone { get; set; }
        public int WorkoutTime { get; set; }
        public DateTime StartWorkoutDate { get; set; }
    }
}
