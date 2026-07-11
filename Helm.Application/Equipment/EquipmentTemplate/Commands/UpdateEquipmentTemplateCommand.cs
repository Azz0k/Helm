using AutoMapper;
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
    [RequireRole("EquipmentManager")]
    public record UpdateEquipmentTemplateCommand : IRequest<GetOperationResult<EquipmentTemplateDTO>>
    {
        public required int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? Enabled { get; set; }
    }
    public class UpdateEquipmentTemplateCommandHandler : IRequestHandler<UpdateEquipmentTemplateCommand, GetOperationResult<EquipmentTemplateDTO>>
    {
        private readonly IEquipmentTemplateRepository equipmentTemplateRepository;
        private readonly IUserRepository userRepository;
        private readonly IUserContext userContext;
        private readonly IMapper mapper;
        public UpdateEquipmentTemplateCommandHandler(IEquipmentTemplateRepository equipmentTemplateRepository,
            IUserRepository userRepository,
            IUserContext userContex,
            IMapper mapper)
        {
            this.equipmentTemplateRepository = equipmentTemplateRepository;
            this.userRepository = userRepository;
            this.userContext = userContex;
            this.mapper = mapper;
        }
        public async Task<GetOperationResult<EquipmentTemplateDTO>> Handle(UpdateEquipmentTemplateCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(userContext.Login))
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
            if (request.Name != null && request.Name != currentTemplate.Name)
            {
                Helm.Domain.Entities.EquipmentTemplate? targetTemplate = await equipmentTemplateRepository.FindEquipmentTemplateByNameAsync(request.Name, cancellationToken);
                if (targetTemplate != null)
                {
                    return new GetOperationResult<EquipmentTemplateDTO>.Conflict();
                }
            }
            currentTemplate.Update(user, request.Name, request.Description, request.Enabled);
            await userRepository.SaveChangesAsync(cancellationToken);
            EquipmentTemplateDTO equipmentTemplateDTO = mapper.Map<EquipmentTemplateDTO>(currentTemplate);
            return new GetOperationResult<EquipmentTemplateDTO>.Success(equipmentTemplateDTO);
        }
    }
}
