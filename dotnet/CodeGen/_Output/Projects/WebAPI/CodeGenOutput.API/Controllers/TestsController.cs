using CodeGenOutput.API.BLL;
using CodeGenOutput.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Controllers
{
    [Route("api/tests")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly ITestBLL _bll;

        public TestsController(IBusinessLogicLayer bll)
        {
            _bll = bll;
        }

        // GET: api/tests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Test>>> GetTests()
        {
            return Ok(await _bll.GetTestsAsync());
        }

        // GET: api/tests/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Test>> GetTestById([FromRoute] Guid id)
        {
            return Ok(await _bll.GetTestByIdAsync(id));
        }

        // GET: api/tests/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Test>>> SearchTest([FromQuery] string term)
        {
            return Ok(await _bll.SearchTestAsync(term));
        }

        // POST: api/tests
        [HttpPost]
        public async Task<ActionResult<Test>> CreateTest([FromBody] Test test)
        {
            Test createdTest = await _bll.CreateTestAsync(test);
            return CreatedAtAction("GetTestById", new { id = createdTest.Id }, createdTest);
        }

        // PUT: api/tests/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Test>> UpdateTest([FromRoute] Guid id, [FromBody] Test test)
        {
            if (id != test.Id) { return BadRequest(); }
            return Ok(await _bll.UpdateTestAsync(test));
        }

        // DELETE: api/tests/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTest(Guid id)
        {
            await _bll.DeleteTestAsync(new Test() { Id = id });
            return NoContent();
        }
    }
}
