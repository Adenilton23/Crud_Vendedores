using SalesWebMvc2.Data;
using SalesWebMvc2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc2.Services
{
    public class DepartmentService
    { // proibir alteracao de dependencia
        private readonly SalesWebMvc2Context _context;

        public DepartmentService(SalesWebMvc2Context context)
        {
            _context = context;
        }
        public async Task<List<Department>> FindAllAsync()
        {   // retornar lista ordenada por nome de todos os departamentos
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}

