using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RubicProg.DataAccess.Core.Models
{
    [Table("Workouts")]
    public class WorkoutRto
    {
        public int Id { get; set; }
        public int UserWhoTrainingId { get; set; }
        public UserRto UserWhoTraining { get; set; }
        public DateTime WorkoutTime { get; set; }
        public string Exercise { get; set; }
        public bool IsDone { get; set; }
        public DateTime StartWorkoutDate { get; set; }
    }
}
