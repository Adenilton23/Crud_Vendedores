using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace SalesWebMvc2.Models
{
    public class Vendedor // Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public  double BaseSalary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<RegistroVenda> Vendas { get; set; } = new List<RegistroVenda>();

        public Vendedor()
        {

        }

        // Não Criar construtor para atributos ICollection
        public Vendedor(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }
        // Adicionar Venda
        public void AddVendas(RegistroVenda rv)
        {
            Vendas.Add(rv);
        }

        // Remover Vendas
        public void RemoveVendas(RegistroVenda rv)
        {
            Vendas.Remove(rv);
        }

        // Somar as vendas de um vendedor em umka determinada data
        public double TotalVendas(DateTime initial, DateTime final)
        {
            return Vendas.Where(rv => rv.Date >= initial && rv.Date <= final).Sum(rv => rv.Amount);
        }


    }
}
