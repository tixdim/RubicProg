﻿namespace RubicProg.API.Models
{
    public class UserUpdateDto
    {
        public string? Nickname { get; set; }
        public string? Password { get; set; }

        public bool IsBoy { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
