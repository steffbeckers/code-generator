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

namespace CodeGenOutput.API.Requests.Contacts
{
    public class UpdateContact : IRequest<Response>
    {
        public ContactUpdateVM ContactUpdateVM { get; set; }
    }

    public class UpdateContactHandler : IRequestHandler<UpdateContact, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateContactHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response> Handle(UpdateContact request, CancellationToken cancellationToken)
        {
            IRepository<Contact> repository = _unitOfWork.GetRepository<Contact>();

            Contact contact = await repository.GetByIdAsync(request.ContactUpdateVM.Id);
            _mapper.Map(request.ContactUpdateVM, contact);

            ValidationResult validationResult = await Validators.ContactValidator.ValidateAsync(contact, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new Response()
                {
                    Success = false,
                    Code = "CONTACT_INVALID",
                    Message = "Contact data invalid",
                    Data = validationResult.Errors
                };
            }

            contact = await repository.UpdateAsync(contact);
            await _unitOfWork.Commit();

            return new Response()
            {
                Code = "CONTACT_UPDATED",
                Message = "Contact updated",
                Data = _mapper.Map<ContactVM>(contact)
            };
        }
    }
}
