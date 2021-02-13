using CodeGenOutput.API.Requests;
using CodeGenOutput.API.Requests.Addresses;
using CodeGenOutput.API.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Controllers
{
    [Route("api/addresses")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddressesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/addresses
        [HttpGet]
        public async Task<IActionResult> GetAddresses([FromQuery] string include = "")
        {
            return Ok(await _mediator.Send(new GetAddresses() { Include = include }));
        }

        // GET: api/addresses/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressById([FromRoute] Guid id, [FromQuery] string include = "")
        {
            return Ok(await _mediator.Send(new GetAddressById() { Id = id, Include = include }));
        }

        // POST: api/addresses
        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] AddressCreateVM addressCreateVM)
        {
            Response response = await _mediator.Send(new CreateAddress() { AddressCreateVM = addressCreateVM });
            return CreatedAtAction("GetAddressById", new { id = (response.Data as AddressVM).Id }, response);
        }

        // PUT: api/addresses/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress([FromRoute] Guid id, [FromBody] AddressUpdateVM addressUpdateVM)
        {
            if (id != addressUpdateVM.Id) { return BadRequest(); }
            return Ok(await _mediator.Send(new UpdateAddress() { AddressUpdateVM = addressUpdateVM }));
        }

        // DELETE: api/addresses/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] Guid id)
        {
            return Ok(await _mediator.Send(new DeleteAddress() { Id = id }));
        }
    }
}
