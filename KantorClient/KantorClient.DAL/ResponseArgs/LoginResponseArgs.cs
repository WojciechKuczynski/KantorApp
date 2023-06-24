using KantorClient.Model;

namespace KantorClient.DAL.ResponseArgs
{
    public class LoginResponseArgs
    {
        public bool Error { get; set; }
        public string ErrorMessage { get; set; }
        public UserSession LoggedSession { get; set; }
    }
}
