using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.Addresses
{
    public class GetAddresses : IRequest<Response>
    {
        public string Include { get; set; }
    }

    public class GetAddressesHandler : IRequestHandler<GetAddresses, Response>
    {
        private readonly IAddressBLL _bll;
        private readonly IMapper _mapper;

        public GetAddressesHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetAddresses request, CancellationToken cancellationToken)
        {
            List<Address> addresses = (await _bll.GetAddressesAsync(request.Include)).ToList();

            return new Response() { Data = _mapper.Map<List<AddressListVM>>(addresses) };
        }
    }
}
