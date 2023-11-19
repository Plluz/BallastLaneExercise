namespace Assignment.Api.Input.Queries
{
    public class RegisterQuery
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
