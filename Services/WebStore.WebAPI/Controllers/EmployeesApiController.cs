using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Interfaces.Services;

namespace WebStore.WebAPI.Controllers
{
    [ApiController]
    [Route("api/employees")]//api/EmployeesApi

    public class EmployeesApiController : ControllerBase
    {
        private readonly IEmployeesData _EmployeesData;
        public EmployeesApiController(IEmployeesData EmployeesData) =>_EmployeesData = EmployeesData;



    }
}
