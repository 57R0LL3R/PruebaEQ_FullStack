using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaEQ_API.Models;
using PruebaEQ_API.Services.Interfaces;

namespace PruebaEQ_API.Services.Implementaciones
{
    // Servicio que maneja la lógica relacionada con los logs de procesamiento.
    // Aquí se definen las operaciones principales para crear y consultar registros.

    public class LogProcessService(EQContext context) : ILogProcessService
    {
        private readonly EQContext _context = context;

        public async Task<ActionResult<IEnumerable<LogProcess>>> GetAll()
        {
            return await _context.LogProcces.ToListAsync();
        }

        public async Task<LogProcess?> Get(int id)
        {
            return await _context.LogProcces.FindAsync(id);
        }

        public async Task<LogProcess?> CreateLogProcces(LogProcess logProcces)
        {
            try
            {
                _context.LogProcces.Add(logProcces);
                await _context.SaveChangesAsync();
                return logProcces;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }

}
