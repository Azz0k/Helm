using FluentValidation;
using Helm.Core.Application.Common;
using Helm.Core.Application.Equipment.Equipment.Queries;
using Helm.Core.Application.Interfaces;
using Helm.Core.Domain.Entities;
using MediatR;

namespace Helm.Core.Application.Equipment.Equipment.Commands
{
    [RequireRole("EquipmentManager")]
    public record CreateEquipmentCommand : IRequest<GetOperationResult<EquipmentDTO>>
    {
        public required string Name { get; set; }
        public bool? IsBulk { get; set; }
    }
    public class CreateEquipmentCommandHandler : IRequestHandler<CreateEquipmentCommand, GetOperationResult<EquipmentDTO>>
    {
        private IEquipmentRepository equipmentRepository;
        private IUserRepository userRepository;
        private readonly IUserContext userContext;
        private readonly IValidator<CreateEquipmentCommand> validator;
        public CreateEquipmentCommandHandler(IEquipmentRepository equipmentRepository, IValidator<CreateEquipmentCommand> validator, IUserContext userContext, IUserRepository userRepository)
        {
            this.validator  = validator;
            this.equipmentRepository = equipmentRepository;
            this.userContext = userContext;
            this.userRepository = userRepository;
        }
        public async Task<GetOperationResult<EquipmentDTO>> Handle(CreateEquipmentCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(userContext.Login))
            {
                return new GetOperationResult<EquipmentDTO>.Unexpected();
            }
            User? user = await userRepository.FindUserByLoginAsync(userContext.Login.ToLower(), cancellationToken);
            if (user == null)
            {
                return new GetOperationResult<EquipmentDTO>.Unexpected();
            }
            if (await equipmentRepository.IsEquipmentExistsAsync(request.Name, cancellationToken))
            {
                return new GetOperationResult<EquipmentDTO>.Conflict();
            }
            Helm.Core.Domain.Entities.Equipment newEquipment = new(user, request.Name, request.IsBulk);
            EquipmentDTO vm = await equipmentRepository.AddEquipmentAsync(newEquipment, cancellationToken);
            return new GetOperationResult<EquipmentDTO>.Success(vm);
        }
    }
}
