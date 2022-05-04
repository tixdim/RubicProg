using System;

namespace RubicProg.BusinessLogic.Core.Models
{
    public class UserRegistrBlo
    {
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string FirstPassword { get; set; }
        public string SecondPassword { get; set; }
        public bool IsBoy { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
