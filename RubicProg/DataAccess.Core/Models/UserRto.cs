using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RubicProg.DataAccess.Core.Models
{
    [Table("Users")]
    public class UserRto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public ProfileUserRto ProgileUser { get; set; }
        public List<WorkoutRto> Workouts { get; set; }
    }
}
