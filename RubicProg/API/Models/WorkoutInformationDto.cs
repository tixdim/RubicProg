using System;

namespace RubicProg.API.Models
{
    public class WorkoutInformationDto
    {
        public int Id { get; set; }
        // public UserInformationDto UserWhoTraining { get; set; }
        public string Exercise { get; set; }
        public bool IsDone { get; set; }
        public int WorkoutTime { get; set; }
        public DateTime StartWorkoutDate { get; set; }
    }
}
