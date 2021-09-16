using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Interfaces.TestAPI;

namespace WebStore.Controllers
{
    public class WebApiController : Controller
    {
        private readonly IValuesService _ValuesService;
        public WebApiController(IValuesService ValuesService)
        {
            _ValuesService = ValuesService;
        }


        public IActionResult Index()
        {
            var values = _ValuesService.GetAll();

            return View(values);
        }
    }
}
