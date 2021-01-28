using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodeGenOutput.API.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        // GET: api/contacts
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        // GET api/contacts/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok();
        }

        // POST api/contacts
        [HttpPost]
        public IActionResult Create([FromBody] ContactVM contact)
        {
            return Ok();
        }

        // PUT api/contacts/{id}
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] ContactVM contact)
        {
            return Ok();
        }

        // DELETE api/contacts/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok();
        }
    }
}
