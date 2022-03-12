using System;
using System.Collections.Generic;
using System.Text;

namespace RubicProg.BusinessLogic.Core.Models
{
    public class UserProfileUpdateBlo
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserWhoProfileId { get; set; }
        public bool IsBoy { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTimeOffset DateRegistration { get; set; }
        public DateTimeOffset Birthday { get; set; }
        public string AvatarUrl { get; set; }
    }
}
