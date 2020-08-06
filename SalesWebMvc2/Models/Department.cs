using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc2.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Vendedor> Vendedores { get; set; } = new List<Vendedor>();

        public Department()
        {

        }
        // Não Criar construtor para atributos ICollection
        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }
        // Adicionar um vendedor
        public void AddVendedor(Vendedor vendedor)
        {   // Adicionar a lista de vendedores um vendedor
            Vendedores.Add(vendedor);
        }

        public double TotalVendas(DateTime initial, DateTime final)
        {          
            return Vendedores.Sum(vendedor => vendedor.TotalVendas(initial, final));

        }
    }
}
