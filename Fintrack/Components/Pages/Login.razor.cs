using Fintrack.Model;

namespace Fintrack.Components.Pages
{
    public partial class Login
    {
        private User Users { get; set; } = new();
        private string ErrorMessage { get; set; } = string.Empty;

        private void HandleLogin()
        {
            if (UserService.Login(Users))
            {
                Nav.NavigateTo("/dashboard");
            }
            else
            {
                ErrorMessage = "Username or password is invalid.";
            }
        }
    }
}
