using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = CodeGenOutput.API.Validation.ValidationException;

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

            AddressValidator validator = new AddressValidator();
            ValidationResult validationResult = await validator.ValidateAsync(address);
            if (!validationResult.IsValid) { throw new ValidationException(validationResult.Errors); }

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
