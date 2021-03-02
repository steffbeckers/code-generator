using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.DAL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.Validation;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAccountHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response> Handle(UpdateAccount request, CancellationToken cancellationToken)
        {
            IRepository<Account> repository = _unitOfWork.GetRepository<Account>();

            Account account = await repository.GetByIdAsync(request.AccountUpdateVM.Id);
            _mapper.Map(request.AccountUpdateVM, account);

            ValidationResult validationResult = await Validators.AccountValidator.ValidateAsync(account, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new Response()
                {
                    Success = false,
                    Code = "ACCOUNT_INVALID",
                    Message = "Account data invalid",
                    Data = validationResult.Errors
                };
            }

            account = await repository.UpdateAsync(account);
            await _unitOfWork.Commit();

            return new Response()
            {
                Code = "ACCOUNT_UPDATED",
                Message = "Account updated",
                Data = _mapper.Map<AccountVM>(account)
            };
        }
    }
}
