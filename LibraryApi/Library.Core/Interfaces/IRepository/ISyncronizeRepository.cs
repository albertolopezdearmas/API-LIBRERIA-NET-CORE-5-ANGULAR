using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Core.Interfaces.IRepository
{
    public interface ISynchronizeRepository : IDisposable
    {
        Task<List<Author>> GetAuthorApi(string path);
        Task<List<Book>> GetBookApi(string path);
    }
}