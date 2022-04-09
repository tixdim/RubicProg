using System;

namespace RubicProg.BusinessLogic.Core.Models
{
    public class UserProfileAddBlo
    {
        public int Id { get; set; }
        public bool IsBoy { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime DateRegistration { get; set; }
        public string AvatarUrl { get; set; }
    }
}
