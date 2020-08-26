using SalesWebMvc2.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc2.Models
{
    public class RegistroVenda //Sales Record
    {
        public int Id { get; set; }        
        public DateTime Date { get; set; }
        public double  Amount { get; set; }
        public StatusVenda Status { get; set; }
        public Vendedor Vendedor { get; set; }

        public RegistroVenda()
        {

        }

        public RegistroVenda(int id, DateTime date, double amount, StatusVenda status, Vendedor vendedor)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Vendedor = vendedor;
        }
    }
}
