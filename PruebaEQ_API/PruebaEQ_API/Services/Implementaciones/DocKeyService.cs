using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaEQ_API.Models;
using PruebaEQ_API.Services.Interfaces;

namespace PruebaEQ_API.Services.Implementaciones
{
    // Servicio encargado de manejar la lógica de negocio para DocKeys.
    // Aquí se definen las operaciones CRUD y se maneja el acceso a la base de datos.

    public class DocKeyService(EQContext context) : IDocKeyService
    {
        private readonly EQContext _context = context;

        public async Task<IEnumerable<DocKey>> GetAll()
        {
            return await _context.DocKeys.ToListAsync();
        }

        public async Task<ActionResult<DocKey>?> Get(int id)
        {
            var docKey = await _context.DocKeys.FindAsync(id);
            return docKey ?? null;
        }

        public async Task<DocKey?> UpdateDocKey(int id, DocKey docKey)
        {
            _context.Entry(docKey).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return docKey;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<DocKey> CreateDocKey(DocKey docKey)
        {
            try
            {
                _context.DocKeys.Add(docKey);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return docKey;
        }

        public async Task<bool> DeleteDocKey(int id)
        {
            var docKey = await _context.DocKeys.FindAsync(id);
            if (docKey == null)
                return false;

            try
            {
                _context.DocKeys.Remove(docKey);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }

}
