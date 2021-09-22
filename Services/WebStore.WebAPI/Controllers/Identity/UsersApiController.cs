using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;

using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces;

namespace WebStore.WebAPI.Controllers.Identity
{
    [ApiController]
    [Route(WebAPIAddresses.Identity.Users)]
    public class UsersApiController : ControllerBase
    {
        public readonly UserStore<User, Role, WebStoreDB> _UserStore;

        public UsersApiController(WebStoreDB db)
        {
            _UserStore = new UserStore<User, Role, WebStoreDB>(db);
            //_UserStore.AutoSaveChanges = false; // выключение отслеживания изменений для повышения быстродействия
        }

        [HttpGet("all")]
        public async Task<IEnumerable<User>> GetAllUsers() => await _UserStore.Users.ToArrayAsync();

    }
}