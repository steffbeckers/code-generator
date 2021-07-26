using AutoMapper;
using CodeGenOutput.API.DAL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.Validation;
using CodeGenOutput.API.ViewModels;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.Accounts
{
    public class CreateAccount : IRequest<Response>
    {
        public AccountCreateVM AccountCreateVM { get; set; }
    }

    public class CreateAccountHandler : IRequestHandler<CreateAccount, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAccountHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response> Handle(CreateAccount request, CancellationToken cancellationToken)
        {
            IRepository<Account> repository = _unitOfWork.GetRepository<Account>();

            Account account = _mapper.Map<Account>(request.AccountCreateVM);

            ValidationResult validationResult = await Validators.AccountValidator.ValidateAsync(account, cancellationToken);
            if (!validationResult.IsValid) {
                return new Response()
                {
                    Success = false,
                    Code = "ACCOUNT_INVALID",
                    Message = "Account data invalid",
                    Data = validationResult.Errors
                };
            }

            account = await repository.CreateAsync(account);
            await _unitOfWork.Commit();

            return new Response()
            {
                Code = "ACCOUNT_CREATED",
                Message = "Account created",
                Data = _mapper.Map<AccountVM>(account)
            };
        }
    }
}
