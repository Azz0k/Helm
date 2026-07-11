using Helm.Application.Common;
using Helm.Application.Equipment.EquipmentTemplate.Queries;
using Helm.Application.Interfaces;
using Helm.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Application.Equipment.EquipmentTemplate.Commands
{
    public record DeleteEquipmentTemplateCommand : IRequest<GetOperationResult<EquipmentTemplateDTO>>
    {
        public int Id { get; set; }
    }
    public class DeleteEquipmentTemplateCommandHndler : IRequestHandler<DeleteEquipmentTemplateCommand, GetOperationResult<EquipmentTemplateDTO>>
    {
        private readonly IEquipmentTemplateRepository equipmentTemplateRepository;
        private readonly IUserRepository userRepository;
        private readonly IUserContext userContext;
        public DeleteEquipmentTemplateCommandHndler(IEquipmentTemplateRepository equipmentTemplateRepository, IUserRepository userRepository, IUserContext userContext)
        {
            this.equipmentTemplateRepository = equipmentTemplateRepository;
            this.userRepository = userRepository;
            this.userContext = userContext;
        }

        public async Task<GetOperationResult<EquipmentTemplateDTO>> Handle(DeleteEquipmentTemplateCommand request, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(userContext.Login))
            {
                return new GetOperationResult<EquipmentTemplateDTO>.Unexpected();
            }
            User? user = await userRepository.FindUserByLoginAsync(userContext.Login.ToLower(), cancellationToken);
            if (user == null)
            {
                return new GetOperationResult<EquipmentTemplateDTO>.Unexpected();
            }
            Helm.Domain.Entities.EquipmentTemplate? currentTemplate = await equipmentTemplateRepository.FindEquipmentTemplateByIdAsync(request.Id, cancellationToken);
            if (currentTemplate == null)
            {
                return new GetOperationResult<EquipmentTemplateDTO>.NotFound();
            }
            currentTemplate.Delete(user);
            await equipmentTemplateRepository.SaveAsync(cancellationToken);
            return new GetOperationResult<EquipmentTemplateDTO>.Success(null);
        }
    }
}
