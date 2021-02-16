using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.Addresses
{
    public class PatchAddress : IRequest<Response>
    {
        public Guid Id { get; set; }
        public JsonPatchDocument<AddressUpdateVM> PatchDocument { get; set; }
    }

    public class PatchAddressHandler : IRequestHandler<PatchAddress, Response>
    {
        private readonly IAddressBLL _bll;
        private readonly IMapper _mapper;

        public PatchAddressHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response> Handle(PatchAddress request, CancellationToken cancellationToken)
        {
            Address address = await _bll.GetAddressByIdAsync(request.Id);
            AddressUpdateVM addressUpdateVM = _mapper.Map<AddressUpdateVM>(address);
            request.PatchDocument.ApplyTo(addressUpdateVM);
            _mapper.Map(addressUpdateVM, address);
            address = await _bll.UpdateAddressAsync(address);

            return new Response()
            {
                Code = "ADDRESS_UPDATED",
                Message = "Address updated",
                Data = _mapper.Map<AddressVM>(address)
            };
        }
    }
}
