using SalesWebMvc2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc2.Models;

namespace SalesWebMvc2.Services
{
    public class RegistroVendaService
    { // proibir alteracao de dependencia
        private readonly SalesWebMvc2Context _context;

        public RegistroVendaService(SalesWebMvc2Context context)
        {
            _context = context;
        }

        // Encontrar por data
        public async Task<List<RegistroVenda>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.RegistroVenda select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result
                // Realiza a junção das tabelas vendor "Using Microsoft .....
                .Include(x => x.Vendedor)
                // Departamento do Vendedor
                .Include(x => x.Vendedor.Department)
                // Ordem Decrescente
                .OrderByDescending(x => x.Date)
                // Cria lista
                .ToListAsync();
        }
    }
}
