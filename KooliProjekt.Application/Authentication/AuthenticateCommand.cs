using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using KooliProjekt.Application.Infrastructure.Results;

namespace KooliProjekt.Application.Authentication
{
    public class AuthenticateCommand : IRequest<OperationResult<string>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
