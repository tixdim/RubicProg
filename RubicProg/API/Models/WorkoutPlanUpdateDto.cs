using System;

namespace RubicProg.API.Models
{
    public class WorkoutPlanUpdateDto
    {
        public string? Exercise { get; set; }
        public DateTime WorkoutTime { get; set; }
    }
}
