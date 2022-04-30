using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RubicProg.DataAccess.Core.Models
{
    [Table("Users")]
    public class UserRto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }

        public bool IsBoy { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateRegistration { get; set; }
        public string AvatarUrl { get; set; }

        public List<WorkoutRto> Workouts { get; set; }
    }
}
