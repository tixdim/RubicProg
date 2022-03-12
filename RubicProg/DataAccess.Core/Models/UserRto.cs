using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RubicProg.DataAccess.Core.Models
{
    public class UserRto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
    }
}
