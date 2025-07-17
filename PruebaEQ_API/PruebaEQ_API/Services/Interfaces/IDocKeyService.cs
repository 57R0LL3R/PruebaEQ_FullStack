using Microsoft.AspNetCore.Mvc;
using PruebaEQ_API.Models;

namespace PruebaEQ_API.Services.Interfaces
{
    // Interface que define los métodos del servicio de claves de documentos.
    // Se usa para desacoplar el controlador de la lógica concreta del servicio.

    public interface IDocKeyService
    {
        Task<IEnumerable<DocKey>> GetAll();
        Task<ActionResult<DocKey>?> Get(int id);
        Task<DocKey?> UpdateDocKey(int id, DocKey docKey);
        Task<DocKey> CreateDocKey(DocKey docKey);
        Task<bool> DeleteDocKey(int id);
    }

}
