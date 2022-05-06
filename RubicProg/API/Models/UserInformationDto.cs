using System;

namespace RubicProg.API.Models
{
    public class UserInformationDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }

        public bool IsBoy { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateRegistration { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
