using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.Addresses
{
    public class CreateAddress : IRequest<Response>
    {
        public AddressCreateVM AddressCreateVM { get; set; }
    }

    public class CreateAddressHandler : IRequestHandler<CreateAddress, Response>
    {
        private readonly IAddressBLL _bll;
        private readonly IMapper _mapper;

        public CreateAddressHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response> Handle(CreateAddress request, CancellationToken cancellationToken)
        {
            Address address = _mapper.Map<Address>(request.AddressCreateVM);
            address = await _bll.CreateAddressAsync(address);

            return new Response()
            {
                Code = "ADDRESS_CREATED",
                Message = "Address created",
                Data = _mapper.Map<AddressVM>(address)
            };
        }
    }
}
