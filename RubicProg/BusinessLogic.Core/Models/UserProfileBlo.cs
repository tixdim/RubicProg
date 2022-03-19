using System;

namespace RubicProg.BusinessLogic.Core.Models
{
    public class UserProfileBlo
    {
        public int Id { get; set; }
        public bool IsBoy { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTimeOffset Birthday { get; set; }
        public string AvatarUrl { get; set; }
    }
}
