using OwlBlog.DAL.Models.Request.Users;

namespace OwlBlog.DAL.Models.Request
{
    public class MainRequest
    {
        public RegisterRequest RegisterView { get; set; }

        public LoginRequest LoginView { get; set; }

        public MainRequest()
        {
            RegisterView = new RegisterRequest();
            LoginView = new LoginRequest();
        }
    }
}
