using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshop
{
    internal class EditCarWorkshopCommandHandler : IRequestHandler<EditCarWorkshopCommand>
    {
        private readonly ICarWorkshopRepository _repository;
        private readonly IUserContext _userContext;

        public EditCarWorkshopCommandHandler(ICarWorkshopRepository repository, IUserContext userContext)
        {
            _repository = repository;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(EditCarWorkshopCommand request, CancellationToken cancellationToken)
        {
            var carWorkshop = await _repository.GetByEncodedName(request.EncodedName!); // pobranie encji z bazy danych

            var user = _userContext.GetCurrentUser();
            var isEditable = user != null && (carWorkshop.CreatedById == user.Id || user.isInRole("Moderator"));

            if (!isEditable)
            {
                return Unit.Value;
            }

            carWorkshop.Description = request.Description;
            carWorkshop.about = request.about;
            carWorkshop.ContactDetails.City = request.City;
            carWorkshop.ContactDetails.PhoneNumber = request.PhoneNumber;
            carWorkshop.ContactDetails.PostalCode = request.PostalCode;
            carWorkshop.ContactDetails.Street = request.Street;

            await _repository.Commit();

            return Unit.Value;
        }
    }
}
