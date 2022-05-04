﻿using System;

namespace RubicProg.BusinessLogic.Core.Models
{
    public class UserUpdateBlo
    {
        public string? Nickname { get; set; }
        public string? Password { get; set; }

        public bool IsBoy { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
