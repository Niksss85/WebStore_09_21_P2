using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

using WebStore.WebAPI.Clients.Base;

namespace WebStore.WebAPI.Clients.Employees
{
    public class EmployeesClient: BaseClient, IEmployeesData
    {
        public EmployeesClient(HttpClient Client): base(Client, WebAPIAddresses.Employees)
        {

        }

        public int Add(Employee employee)
        {
            var response = Post(Address, employee);
            var id = response.Content.ReadFromJsonAsync<int>().Result;
            return id;
        }

        public bool Delete(int id)
        {
            var response = Delete($"{Address}/{id}");
            var success = response.IsSuccessStatusCode;
            return success;
            //return response.Content.ReadFromJsonAsync<bool>().Result;
        }

        public Employee Get(int id)
        {
            var result = Get<Employee>($"{Address}/{id}");
            return result;
        }

        public IEnumerable<Employee> GetAll()
        {
            var result = Get<IEnumerable<Employee>>(Address);
            return result;

        }

        public void Update(Employee employee)
        {
            Put(Address, employee);
        }
    }
}
