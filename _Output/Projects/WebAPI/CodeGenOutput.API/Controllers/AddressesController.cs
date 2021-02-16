using CodeGenOutput.API.Requests;
using CodeGenOutput.API.Requests.Addresses;
using CodeGenOutput.API.ViewModels;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
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
        [HttpGet]
        [Route("{id}")]
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
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAddress([FromRoute] Guid id, [FromBody] AddressUpdateVM addressUpdateVM)
        {
            if (id != addressUpdateVM.Id) { return BadRequest(); }
            return Ok(await _mediator.Send(new UpdateAddress() { AddressUpdateVM = addressUpdateVM }));
        }

        // PATCH: api/addresses/{id}
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> PatchAddress([FromRoute] Guid id, [FromBody] JsonPatchDocument<AddressUpdateVM> addressPatchDocument)
        {
            return Ok(await _mediator.Send(new PatchAddress() { Id = id, PatchDocument = addressPatchDocument }));
        }

        // DELETE: api/addresses/{id}
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] Guid id)
        {
            return Ok(await _mediator.Send(new DeleteAddress() { Id = id }));
        }
    }
}
