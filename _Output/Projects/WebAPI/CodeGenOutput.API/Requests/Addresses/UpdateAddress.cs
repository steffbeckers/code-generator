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
    public class UpdateAddress : IRequest<Response>
    {
        public AddressUpdateVM AddressUpdateVM { get; set; }
    }

    public class UpdateAddressHandler : IRequestHandler<UpdateAddress, Response>
    {
        private readonly IAddressBLL _bll;
        private readonly IMapper _mapper;

        public UpdateAddressHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response> Handle(UpdateAddress request, CancellationToken cancellationToken)
        {
            Address address = _mapper.Map<Address>(request.AddressUpdateVM);

            AddressValidator validator = new AddressValidator();
            ValidationResult validationResult = await validator.ValidateAsync(address);
            if (!validationResult.IsValid) { throw new ValidationException(validationResult.Errors); }

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