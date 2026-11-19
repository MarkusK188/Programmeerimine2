using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Infrastructure.Files
{
    public interface IFileClient
    {
        Task Save(string fileName, string fileStore, Stream inputStream);
        Task Delete(string fileName, string fileStore);
       Task<Stream> Get(string fileName, string fileStore);
        //Task List();
    }
}