using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Interfaces.Services.Interfaces
{
    public interface IUsersClient:
        IUserRoleStore<User>, 
        IUserPasswordStore<User>,
        IUserEmailStore<User>,
        IUserPhoneNumberStore<User>,
        IUserTwoFactorStore<User>,
        IUserLoginStore<User>,
        IUserClaimStore<User>
    {

    }
}
