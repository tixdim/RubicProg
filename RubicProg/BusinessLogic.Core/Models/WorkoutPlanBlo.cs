using System;
using System.Collections.Generic;
using System.Text;

namespace RubicProg.BusinessLogic.Core.Models
{
    public class WorkoutPlanBlo
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserWhoTrainingId { get; set; }
        public int WorkoutTime { get; set; }
        public string Exercise { get; set; }
        public bool IsDone { get; set; }
        public DateTimeOffset StartWorkoutDate { get; set; }
    }
}
