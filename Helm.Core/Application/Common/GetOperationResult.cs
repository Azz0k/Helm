using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Application.Common
{
    public abstract record GetOperationResult<T>
    {
        public sealed record Success(T Data) : GetOperationResult<T>;
        public sealed record NotFound : GetOperationResult<T>;
        public sealed record Invalid : GetOperationResult<T>;
        public sealed record Forbidden : GetOperationResult<T>;
        public sealed record Conflict : GetOperationResult<T>;
    }
}
