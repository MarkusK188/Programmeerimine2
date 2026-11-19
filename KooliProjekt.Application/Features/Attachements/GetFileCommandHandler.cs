using System.IO;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Files;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Attachements
{
    
        public class GetFileCommandHandler :
            IRequestHandler<GetFileCommand, Stream>
        {
            private readonly ApplicationDbContext _context;
            private readonly IFileClient _fileClient;

            public GetFileCommandHandler(
                ApplicationDbContext context,
                IFileClient fileClient)
            {
                _context = context;
                _fileClient = fileClient;
            }

            public async Task<Stream> Handle(
                GetFileCommand request,
                CancellationToken cancellationToken)
            {
                var attachment = await _context.Attachements
                    .FirstOrDefaultAsync(
                        a => a.Id == request.Id,
                        cancellationToken);

                if (attachment == null)
                    throw new FileNotFoundException("Attachment not found.");

                return await _fileClient.Get(
                    Path.GetFileName(attachment.FilePath),
                    Path.GetDirectoryName(attachment.FilePath));
            }
        }
    }
