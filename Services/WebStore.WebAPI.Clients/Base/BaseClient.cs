using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebStore.WebAPI.Base
{
    public abstract class BaseClient
    {
        protected HttpClient Http { get; }
        protected string Address { get; }
        protected BaseClient(HttpClient Client, string Address)
        {
            Http = Client;
            this.Address = Address;
        }
    }
}
