using Microsoft.AspNetCore.Mvc;
using PruebaEQ_API.Models;

namespace PruebaEQ_API.Services.Interfaces
{
    public interface IDocKeyService
    {
        public Task<IEnumerable<DocKey>> GetAll();
        public Task<ActionResult<DocKey>?> Get(int id);
        public Task<DocKey?> UpdateDocKey(int id, DocKey docKey);
        public Task<DocKey> CreateDocKey(DocKey docKey);
        public Task<bool> DeleteDocKey(int id);
    }
}
