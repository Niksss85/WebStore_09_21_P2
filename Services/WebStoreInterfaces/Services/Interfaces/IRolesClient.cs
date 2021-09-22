using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Interfaces.Services.Interfaces
{
    public interface IRolesClient : IRoleStore<Role> { }
}
