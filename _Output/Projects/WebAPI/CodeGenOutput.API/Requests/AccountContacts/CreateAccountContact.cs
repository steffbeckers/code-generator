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
    public class CreateAccountContact : IRequest<Response>
    {
        public AccountContactCreateVM AccountContactCreateVM { get; set; }
    }

    public class CreateAccountContactHandler : IRequestHandler<CreateAccountContact, Response>
    {
        private readonly IAccountContactBLL _bll;
        private readonly IMapper _mapper;

        public CreateAccountContactHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response> Handle(CreateAccountContact request, CancellationToken cancellationToken)
        {
            AccountContact accountcontact = _mapper.Map<AccountContact>(request.AccountContactCreateVM);

            AccountContactValidator validator = new AccountContactValidator();
            ValidationResult validationResult = await validator.ValidateAsync(accountcontact);
            if (!validationResult.IsValid) { throw new ValidationException(validationResult.Errors); }

            accountcontact = await _bll.CreateAccountContactAsync(accountcontact);

            return new Response()
            {
                Code = "ACCOUNTCONTACT_CREATED",
                Message = "AccountContact created",
                Data = _mapper.Map<AccountContactVM>(accountcontact)
            };
        }
    }
}
