using FluentValidation;
using Helm.Core.Application.Common;
using Helm.Core.Application.Equipment.Equipment.Queries;
using Helm.Core.Application.Equipment.EquipmentTemplate.Queries;
using Helm.Core.Application.Interfaces;
using Helm.Core.Domain.Entities;
using MediatR;


namespace Helm.Core.Application.Equipment.EquipmentTemplate.Commands
{
    [RequireRole("EquipmentManager")]
    public record CreateEquipmentTemplateCommand : IRequest<GetOperationResult<EquipmentTemplateDTO>>
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string RenderTemplateKey { get; set; }
    }
    public class CreateEquipmentTemplateCommandHandler : IRequestHandler<CreateEquipmentTemplateCommand, GetOperationResult<EquipmentTemplateDTO>>
    {
        private readonly IEquipmentTemplateRepository equipmentTemplateRepository;
        private readonly IUserRepository userRepository;
        private readonly IUserContext userContext;
        public CreateEquipmentTemplateCommandHandler(IEquipmentTemplateRepository equipmentTemplateRepository,
            IUserRepository userRepository,
            IUserContext userContex)
        {
            this.equipmentTemplateRepository = equipmentTemplateRepository;
            this.userRepository = userRepository;
            this.userContext = userContex;
        }
        public async Task<GetOperationResult<EquipmentTemplateDTO>> Handle(CreateEquipmentTemplateCommand request, CancellationToken cancellationToken)
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
            if (await equipmentTemplateRepository.IsEquipmentTemplateExistsAsync(request.Name, cancellationToken))
            {
                return new GetOperationResult<EquipmentTemplateDTO>.Conflict();
            }
            Helm.Core.Domain.Entities.EquipmentTemplate newTemplate = new(user,request.Name, request.RenderTemplateKey, request.Description);
            EquipmentTemplateDTO dto = await equipmentTemplateRepository.AddEquipmentTemplateAsync(newTemplate, cancellationToken);
            return new GetOperationResult<EquipmentTemplateDTO>.Success(dto);
        }
    }
}
