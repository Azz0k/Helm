using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Helm.Core.Application.Common;
using Helm.Core.Application.Equipment.Equipment.Queries;
using Helm.Core.Application.Interfaces;
using Helm.Core.Domain.Entities;
using MediatR;

namespace Helm.Core.Application.Equipment.Equipment.Commands
{
    [RequireRole("EquipmentManager")]
    public record RenameEquipmentCommand :IRequest<GetOperationResult<EquipmentDTO>>
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
    }
    public class RenameEquipmentCommandHandler : IRequestHandler<RenameEquipmentCommand, GetOperationResult<EquipmentDTO>>
    {
        private readonly IEquipmentRepository equipmentRepository;
        private readonly IUserRepository userRepository;
        private readonly IUserContext userContext;
        private readonly IValidator<RenameEquipmentCommand> validator;
        private readonly IMapper mapper;
        public RenameEquipmentCommandHandler(IEquipmentRepository equipmentRepository, 
            IUserRepository userRepository, 
            IUserContext userContext, 
            IValidator<RenameEquipmentCommand> validator,
            IMapper mapper)
        {
            this.equipmentRepository = equipmentRepository;
            this.userRepository = userRepository;
            this.userContext = userContext;
            this.validator = validator;
            this.mapper = mapper;
        }

        public async Task<GetOperationResult<EquipmentDTO>> Handle(RenameEquipmentCommand request, CancellationToken cancellationToken)
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
            Helm.Core.Domain.Entities.Equipment? equipment = await equipmentRepository.FindEquipmentByIdAsync(request.Id, cancellationToken);
            if (equipment == null)
            {
                return new GetOperationResult<EquipmentDTO>.NotFound();
            }
            equipment.Rename(user, request.Name);
            await equipmentRepository.SaveAsync(cancellationToken);
            EquipmentDTO equipmentDto =  mapper.Map<EquipmentDTO>(equipment);
            return new GetOperationResult<EquipmentDTO>.Success(equipmentDto);
        }
    }
}
