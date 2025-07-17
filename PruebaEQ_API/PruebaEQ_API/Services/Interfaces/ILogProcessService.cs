using Microsoft.AspNetCore.Mvc;
using PruebaEQ_API.Models;

namespace PruebaEQ_API.Services.Interfaces
{

     // Interface para el servicio de logs de procesos.
     // Permite extender y abstraer la lógica de los logs sin acoplarse al controlador.

    public interface ILogProcessService
    {
        Task<ActionResult<IEnumerable<LogProcess>>> GetAll();
        Task<LogProcess?> Get(int id);
        Task<LogProcess?> CreateLogProcces(LogProcess logProcces);
    }

}
