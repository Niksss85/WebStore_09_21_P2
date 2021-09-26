using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Interfaces;

namespace WebStore.WebAPI.Controllers
{
    [Route(WebAPIAddresses.Values)]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static List<string> _Values = Enumerable
            .Range(1, 10)
            .Select(i => $"Value-{i}")
            .ToList();
        public ValuesController()
        {

        }

        [HttpGet]
        public IActionResult Get() => Ok(_Values);
        [HttpGet("count")]
        public IActionResult Count() => Ok(_Values.Count);
        [HttpGet("{index}")]
        [HttpGet("index[[{index}]]")]
        public ActionResult<string> GetByIndex(int index)
        {
            if (index < 0)
                return BadRequest();
            if (index > _Values.Count)
                return NotFound();
            return _Values[index];
        }
        [HttpPost]
        [HttpPost("add")]
        public ActionResult Add(string str)
        {
            _Values.Add(str);
            var index = _Values.Count - 1;
            return CreatedAtAction(nameof(GetByIndex), new { index = index });
        }
        [HttpPut]
        [HttpPut("edit/{index}")]
        public ActionResult Replace(int index, string NewStr)
        {
            if (index < 0)
                return BadRequest();
            if (index > _Values.Count)
                return NotFound();
            _Values[index] = NewStr;
            return Ok();
        }
        [HttpDelete("{index}")]

        public ActionResult Delete(int index)
        {
            if (index < 0)
                return BadRequest();
            if (index > _Values.Count)
                return NotFound();
            _Values.RemoveAt(index);
            return Ok();
        }
    }
}
