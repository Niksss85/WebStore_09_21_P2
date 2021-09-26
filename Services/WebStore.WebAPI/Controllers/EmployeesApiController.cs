using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.WebAPI.Controllers
{
    /// <summary>API для управления сотрудниками</summary>
    [ApiController]
    [Route(WebAPIAddresses.Employees)]//api/EmployeesApi

    public class EmployeesApiController : ControllerBase
    {
        private readonly IEmployeesData _EmployeesData;
        public EmployeesApiController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;

        /// <summary>Получение всех сотрудников</summary>
        /// <returns>Список сотрудников</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Employee>))]
        public IActionResult Get()
        {
            var employees = _EmployeesData.GetAll();
            return Ok(employees);
        }

        /// <summary>Получение сотрудника по его идентификатору</summary>
        /// <param name="id">Идентификатор сотрудника</param>
        /// <returns>Информация о сотруднике с указанным идентификатором</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(int))]
        public IActionResult GetById(int id)
        {
            var employee = _EmployeesData.Get(id);
            if (employee is null)
                return NotFound(new { id });
            return Ok(employee);
        }

        /// <summary>Добавление нового сотрудника</summary>
        /// <param name="employee">Информация о добавляемом сотруднике</param>
        /// <returns>Идентификатор добавленного сотрудника</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public IActionResult Add(Employee employee)
        {
            var id = _EmployeesData.Add(employee);
            return Ok(id);//CreatedAtAction(nameof(GetById) , new { id });
        }
        /// <summary>Редактирование сотрудника</summary>
        /// <param name="employee">Информация, которую надо внести в запись указанного сотрудника</param>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Update(Employee employee)
        {
            _EmployeesData.Update(employee);
            return Ok();
        }
        /// <summary>Удаление сотрудника</summary>
        /// <param name="id">Идентификатор удаляемого сотрудника</param>
        /// <returns>Истина, если сотрудник успешно удалён</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(bool))]
        public IActionResult Delete(int id)
        {
            var result = _EmployeesData.Delete(id);
            return result ? Ok(true) : NotFound(false);
        }

    }
}
