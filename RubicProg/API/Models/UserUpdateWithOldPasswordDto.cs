namespace RubicProg.API.Models
{
    public class UserUpdateWithOldPasswordDto
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
