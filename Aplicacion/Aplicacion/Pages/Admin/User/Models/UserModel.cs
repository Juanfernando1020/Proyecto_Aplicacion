namespace Aplicacion.Pages.Admin.User.Models
{
    public class UserModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Location { get; set; }
        public bool IsAdmin { get; set; }
    }
}
