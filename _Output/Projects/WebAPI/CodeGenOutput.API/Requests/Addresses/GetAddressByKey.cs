using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.Addresses
{
    public class GetAddressById : IRequest<Response>
    {
        public Guid Id { get; set; }
        public string Include { get; set; }
    }

    public class GetAddressByIdHandler : IRequestHandler<GetAddressById, Response>
    {
        private readonly IAddressBLL _bll;
        private readonly IMapper _mapper;

        public GetAddressByIdHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetAddressById request, CancellationToken cancellationToken)
        {
            Address address = await _bll.GetAddressByIdAsync(request.Id, request.Include);
            if (address == null)
            {
                return new Response() { Success = false, Message = $"Address {request.Id} not found." };
            }

            return new Response() { Data = _mapper.Map<AddressVM>(address) };
        }
    }
}
