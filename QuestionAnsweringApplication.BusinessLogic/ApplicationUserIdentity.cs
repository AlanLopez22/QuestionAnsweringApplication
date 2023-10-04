using System.Security.Claims;
using System.Security.Principal;

namespace QuestionAnsweringApplication.BusinessLogic
{
    public sealed class ApplicationUserIdentity : ClaimsIdentity, IIdentity
    {
        public ApplicationUserIdentity()
        {
        }
    }
}
