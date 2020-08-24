﻿using SalesWebMvc2.Data;
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
        public List<Vendedor> FindAll()
        {   // retornar lista de todos vendedores
            return _context.Vendedor.ToList();
        }
         public void Insert(Vendedor obj)
         {            
            _context.Add(obj);
            _context.SaveChanges();
         }

        public Vendedor FindById(int id)
        {
            return _context.Vendedor.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);

        }

        public void Remove(int id)
        {
            var obj = _context.Vendedor.Find(id);
            _context.Vendedor.Remove(obj);
            _context.SaveChanges();
        }
        public void Update(Vendedor obj)
        {
            if (!_context.Vendedor.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id Not Found");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }


        }


    }
}
