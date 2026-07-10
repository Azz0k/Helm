using AutoMapper;
using FluentValidation;
using Helm.Core.Application.Common;
using Helm.Core.Application.Equipment.Equipment.Queries;
using Helm.Core.Application.Interfaces;
using Helm.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Application.Equipment.Equipment.Commands
{
    [RequireRole("EquipmentManager")]
    public record ReturnEquipmentCommand : IRequest<GetOperationResult<EquipmentDTO>>
    {
        public required int Id { get; set; }
    }
    public class ReturnEquipmentCommandHandler : IRequestHandler<ReturnEquipmentCommand, GetOperationResult<EquipmentDTO>>
    {
        private readonly IEquipmentRepository equipmentRepository;
        private readonly IUserRepository userRepository;
        private readonly IUserContext userContext;
        private readonly IMapper mapper;
        public ReturnEquipmentCommandHandler(IEquipmentRepository equipmentRepository, IUserRepository userRepository, IUserContext userContext, IMapper mapper)
        {
            this.equipmentRepository = equipmentRepository;
            this.userRepository = userRepository;
            this.userContext = userContext;
            this.mapper = mapper;
        }
        public async Task<GetOperationResult<EquipmentDTO>> Handle(ReturnEquipmentCommand request, CancellationToken cancellationToken)
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
            Domain.Entities.Equipment? equipment = await equipmentRepository.FindEquipmentByIdAsync(request.Id, cancellationToken);
            if (equipment == null)
            {
                return new GetOperationResult<EquipmentDTO>.NotFound();
            }
            equipment.Return(user);
            await equipmentRepository.SaveAsync(cancellationToken);
            EquipmentDTO equipmentDto = mapper.Map<EquipmentDTO>(equipment);
            return new GetOperationResult<EquipmentDTO>.Success(equipmentDto);
        }
    }
}
