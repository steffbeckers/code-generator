using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodeGenOutput.API.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        // GET: api/accounts
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        // GET api/accounts/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok();
        }

        // POST api/accounts
        [HttpPost]
        public IActionResult Create([FromBody] AccountVM account)
        {
            return Ok();
        }

        // PUT api/accounts/{id}
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] AccountVM account)
        {
            return Ok();
        }

        // DELETE api/accounts/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok();
        }
    }
}
