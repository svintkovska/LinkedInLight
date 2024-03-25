using Domain.Models;

namespace BLL.ViewModels.AuthModels
{
    public class LoginResultVM
    {
        public bool Success { get; set; }
        public bool EmailNotConfirmed { get; set; }
        public ApplicationUser User { get; set; }
        public IList<string> Roles { get; set; }
        public string Token { get; set; }
    }
}
