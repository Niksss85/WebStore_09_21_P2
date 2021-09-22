using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces;

namespace WebStore.WebAPI.Controllers.Identity
{
    [ApiController]
    [Route(WebAPIAddresses.Identity.Roles)]
    public class RolesApiController : ControllerBase
    {
        private readonly RoleStore<Role> _RoleStore;

        public RolesApiController(WebStoreDB db)
        {
            _RoleStore = new RoleStore<Role>(db);
        }

        [HttpGet("all")]
        public async Task<IEnumerable<Role>> GetAllUsers() => await _RoleStore.Roles.ToArrayAsync();
    }
}
