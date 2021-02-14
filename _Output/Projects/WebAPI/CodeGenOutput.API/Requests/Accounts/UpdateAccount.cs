using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = CodeGenOutput.API.Validation.ValidationException;

namespace CodeGenOutput.API.Requests.Accounts
{
    public class UpdateAccount : IRequest<Response>
    {
        public AccountUpdateVM AccountUpdateVM { get; set; }
    }

    public class UpdateAccountHandler : IRequestHandler<UpdateAccount, Response>
    {
        private readonly IAccountBLL _bll;
        private readonly IMapper _mapper;

        public UpdateAccountHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response> Handle(UpdateAccount request, CancellationToken cancellationToken)
        {
            Account account = _mapper.Map<Account>(request.AccountUpdateVM);

            AccountValidator validator = new AccountValidator();
            ValidationResult validationResult = await validator.ValidateAsync(account);
            if (!validationResult.IsValid) { throw new ValidationException(validationResult.Errors); }

            account = await _bll.UpdateAccountAsync(account);

            return new Response()
            {
                Code = "ACCOUNT_UPDATED",
                Message = "Account updated",
                Data = _mapper.Map<AccountVM>(account)
            };
        }
    }
}
