using Helm.Core.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;


namespace Helm.Core.Application.Common.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly IUserContext _userContext;
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationBehaviour(IUserContext userContext, IAuthorizationService authorizationService)
        {
            _userContext = userContext;
            _authorizationService = authorizationService;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var isAllowed = await _authorizationService.IsAllowedAsync(_userContext.Login, typeof(TRequest), cancellationToken);
            if (!isAllowed)
            {
                throw new Exceptions.ForbiddenException();
            }
            return await next();
        }
    }
}
