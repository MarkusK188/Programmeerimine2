using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Infrastructure.Files
{
    public class LocalFileClient : IFileClient
    {
        private readonly string _filesRoot;

        public LocalFileClient(string filesRoot)
        {
            _filesRoot = filesRoot;
        }

        public async Task Save(string fileName, string fileStore, Stream inputStream)
        {
            var folderPath = Path.Combine(_filesRoot, fileStore);
            var filePath = Path.Combine(folderPath, fileName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using var fileStream = new FileStream(filePath, FileMode.Create);
            await inputStream.CopyToAsync(fileStream);
        }

            public Task Delete(string fileName, string fileStore)
        {
            var filePath = Path.Combine(_filesRoot, fileStore, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            return Task.CompletedTask;
        }
        public Task<Stream> Get(string fileName, string fileStore)
        {
            var filePath = Path.Combine(_filesRoot, fileStore, fileName);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found.", fileName);
            }

            Stream stream = new FileStream(
                filePath,
                FileMode.Open,
                FileAccess.Read);

            return Task.FromResult(stream);
        }

    }
}
