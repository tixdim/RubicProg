using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RubicProg.DataAccess.Core.Models
{
    [Table("Users")]
    public class UserRto
    {
        [Key] public int Id { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Nickname { get; set; }
        [Required, MinLength(6)] public string Password { get; set; }

        public bool IsBoy { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateRegistration { get; set; }
        public string? AvatarUrl { get; set; }

        public List<WorkoutRto> Workouts { get; set; }
    }
}
