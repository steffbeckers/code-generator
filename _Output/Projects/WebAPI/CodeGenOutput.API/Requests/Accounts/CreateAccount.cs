using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = CodeGenOutput.API.Validation.ValidationException;

namespace CodeGenOutput.API.Requests.Accounts
{
    public class CreateAccount : IRequest<Response>
    {
        public AccountCreateVM AccountCreateVM { get; set; }
    }

    public class CreateAccountHandler : IRequestHandler<CreateAccount, Response>
    {
        private readonly IAccountBLL _bll;
        private readonly IMapper _mapper;

        public CreateAccountHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response> Handle(CreateAccount request, CancellationToken cancellationToken)
        {
            Account account = _mapper.Map<Account>(request.AccountCreateVM);

            AccountValidator validator = new AccountValidator();
            ValidationResult validationResult = await validator.ValidateAsync(account);
            if (!validationResult.IsValid) { throw new ValidationException(validationResult.Errors); }

            account = await _bll.CreateAccountAsync(account);

            return new Response()
            {
                Code = "ACCOUNT_CREATED",
                Message = "Account created",
                Data = _mapper.Map<AccountVM>(account)
            };
        }
    }
}
