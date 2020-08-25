using SalesWebMvc2.Data;
using SalesWebMvc2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc2.Services.Exceptions;

namespace SalesWebMvc2.Services
{
    public class VendedorService
    {
        // proibir alteracao de dependencia
        private readonly SalesWebMvc2Context _context;

        public VendedorService (SalesWebMvc2Context context)
        {
            _context = context;
        }
        public async Task<List<Vendedor>> FindAllAsync()
        {   // retornar lista de todos vendedores
            return await _context.Vendedor.ToListAsync();
        }
         public async Task InsertAsync (Vendedor obj)
         {            
            _context.Add(obj);
            await _context.SaveChangesAsync();
         }

        public async Task<Vendedor> FindByIdAsync(int id)
        {
            return await _context.Vendedor.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);

        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Vendedor.FindAsync(id);
                _context.Vendedor.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException("Impossivel Deletar Vendedor que possui vendas");
            }

        }
        public async Task UpdateAsync(Vendedor obj)
        {
            bool hasAny = await _context.Vendedor.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id Not Found");
            }
            try
            {
                _context.Update(obj);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
           

        }


    


    }
}
