using AutoMapper;
using CodeGenOutput.API.DAL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.Validation;
using CodeGenOutput.API.ViewModels;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.Contacts
{
    public class CreateContact : IRequest<Response>
    {
        public ContactCreateVM ContactCreateVM { get; set; }
    }

    public class CreateContactHandler : IRequestHandler<CreateContact, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateContactHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response> Handle(CreateContact request, CancellationToken cancellationToken)
        {
            IRepository<Contact> repository = _unitOfWork.GetRepository<Contact>();

            Contact contact = _mapper.Map<Contact>(request.ContactCreateVM);

            ValidationResult validationResult = await Validators.ContactValidator.ValidateAsync(contact, cancellationToken);
            if (!validationResult.IsValid) {
                return new Response()
                {
                    Success = false,
                    Code = "CONTACT_INVALID",
                    Message = "Contact data invalid",
                    Data = validationResult.Errors
                };
            }

            contact = await repository.CreateAsync(contact);
            await _unitOfWork.Commit();

            return new Response()
            {
                Code = "CONTACT_CREATED",
                Message = "Contact created",
                Data = _mapper.Map<ContactVM>(contact)
            };
        }
    }
}
