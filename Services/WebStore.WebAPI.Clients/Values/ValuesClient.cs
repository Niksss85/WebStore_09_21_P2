using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebStore.Interfaces.TestAPI;
using WebStore.WebAPI.Base;

namespace WebStore.WebAPI.Values
{
    public class ValuesClient : BaseClient, IValuesService
    {
        public ValuesClient(HttpClient Client) : base(Client, "api/values")
        {

        }
        public void Add(string Value)
        {
            var response = Http.PostAsJsonAsync(Address, Value).Result;
            response.EnsureSuccessStatusCode();
        }

        public bool Delete(int index)
        {
            var response = Http.DeleteAsync($"{Address}/{index}").Result;
            return response.IsSuccessStatusCode;

        }

        public void Edit(int index, string str)
        {
            var response = Http.PutAsJsonAsync($"{Address}/{index}", str).Result;
            response.EnsureSuccessStatusCode();
        }

        public string GertByIndex(int index)
        {
            var response = Http.GetAsync($"{Address}/{index}").Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadFromJsonAsync<string>().Result;
            return "";
        }

        public IEnumerable<string> GetAll()
        {
            var response = Http.GetAsync(Address).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadFromJsonAsync<IEnumerable<string>>().Result;
            return Enumerable.Empty<string>();
        }
    }
}