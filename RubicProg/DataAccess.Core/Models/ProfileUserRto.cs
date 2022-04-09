using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RubicProg.DataAccess.Core.Models
{
    [Table("ProfileUsers")]
    public class ProfileUserRto
    {
        public int Id { get; set; }
        public int UserWhoProfileId { get; set; }
        public UserRto UserWhoProfile { get; set; }
        public bool IsBoy { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateRegistration { get; set; }
        public DateTime Birthday { get; set; }
        public string AvatarUrl { get; set; }
    }
}
