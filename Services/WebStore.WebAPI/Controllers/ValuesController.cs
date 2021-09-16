using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace WebStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static List<string> _Values = Enumerable
            .Range(1, 10)
            .Select(i => $"Value-{i}")
            .ToList();

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
