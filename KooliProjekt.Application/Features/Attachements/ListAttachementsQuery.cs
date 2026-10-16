using System.Collections.Generic;
using KooliProjekt.Application.Data;
using MediatR;

namespace KooliProjekt.Application.Features.Attachements
{
    public class ListAttachementsQuery : IRequest<IEnumerable<Attachement>>
    {
    }
}
