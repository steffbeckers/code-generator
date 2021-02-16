using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

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
            accountcontact = await _bll.CreateAccountContactAsync(accountcontact);

            return new Response()
            {
                Code = "ACCOUNTCONTACT_CREATED",
                Message = "Account contact link created",
                Data = _mapper.Map<AccountContactVM>(accountcontact)
            };
        }
    }
}
