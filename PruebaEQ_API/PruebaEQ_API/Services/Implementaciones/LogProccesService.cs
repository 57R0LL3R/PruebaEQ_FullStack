using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaEQ_API.Models;
using PruebaEQ_API.Services.Interfaces;

namespace PruebaEQ_API.Services.Implementaciones
{
    public class LogProccesService(EQContext context) :ILogProccesService
    {
        private readonly EQContext _context = context;
        public async Task<ActionResult<IEnumerable<LogProcces>>> GetAll()
        {
            return await _context.LogProcces.ToListAsync();
        }

        public async Task<LogProcces?> Get(int id)
        {
            var logProcces = await _context.LogProcces.FindAsync(id);
            return logProcces;
        }

        public async Task<LogProcces?> CreateLogProcces(LogProcces logProcces)
        {
            try
            {
                _context.LogProcces.Add(logProcces);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
            return logProcces;
        }

    }
}
