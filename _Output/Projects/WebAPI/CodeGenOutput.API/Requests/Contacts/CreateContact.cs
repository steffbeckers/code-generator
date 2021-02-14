using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = CodeGenOutput.API.Validation.ValidationException;

namespace CodeGenOutput.API.Requests.Contacts
{
    public class CreateContact : IRequest<Response>
    {
        public ContactCreateVM ContactCreateVM { get; set; }
    }

    public class CreateContactHandler : IRequestHandler<CreateContact, Response>
    {
        private readonly IContactBLL _bll;
        private readonly IMapper _mapper;

        public CreateContactHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response> Handle(CreateContact request, CancellationToken cancellationToken)
        {
            Contact contact = _mapper.Map<Contact>(request.ContactCreateVM);

            ContactValidator validator = new ContactValidator();
            ValidationResult validationResult = await validator.ValidateAsync(contact);
            if (!validationResult.IsValid) { throw new ValidationException(validationResult.Errors); }

            contact = await _bll.CreateContactAsync(contact);

            return new Response()
            {
                Code = "CONTACT_CREATED",
                Message = "Contact created",
                Data = _mapper.Map<ContactVM>(contact)
            };
        }
    }
}
