namespace RubicProg.API.Models
{
    public class UserRegistrDto
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
