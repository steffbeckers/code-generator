using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = CodeGenOutput.API.Validation.ValidationException;

namespace CodeGenOutput.API.Requests.AccountContacts
{
    public class UpdateAccountContact : IRequest<Response>
    {
        public AccountContactUpdateVM AccountContactUpdateVM { get; set; }
    }

    public class UpdateAccountContactHandler : IRequestHandler<UpdateAccountContact, Response>
    {
        private readonly IAccountContactBLL _bll;
        private readonly IMapper _mapper;

        public UpdateAccountContactHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response> Handle(UpdateAccountContact request, CancellationToken cancellationToken)
        {
            AccountContact accountcontact = _mapper.Map<AccountContact>(request.AccountContactUpdateVM);

            AccountContactValidator validator = new AccountContactValidator();
            ValidationResult validationResult = await validator.ValidateAsync(accountcontact);
            if (!validationResult.IsValid) { throw new ValidationException(validationResult.Errors); }

            accountcontact = await _bll.UpdateAccountContactAsync(accountcontact);

            return new Response()
            {
                Code = "ACCOUNTCONTACT_UPDATED",
                Message = "AccountContact updated",
                Data = _mapper.Map<AccountContactVM>(accountcontact)
            };
        }
    }
}