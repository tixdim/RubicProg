using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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
        public DateTimeOffset DateRegistration { get; set; }
        public DateTimeOffset Birthday { get; set; }
        public string AvatarUrl { get; set; }
    }
}
