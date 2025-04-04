 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Entities
{
    public class Order
    {
        #region Constructor
        protected Order() { }

        public Order(string cliente, string produto, decimal valor)
        {
            Cliente = cliente;
            Produto = produto;
            Valor = valor;
            Status = "Pendente";
            DataCriacao = DateTime.UtcNow;


        }

        public Order(Guid id, string cliente, string produto, decimal valor, string status, DateTime dataCriacao)
        {
            Id = id;
            Cliente = cliente;
            Produto = produto;
            Valor = valor;
            Status = status;
            DataCriacao = dataCriacao;
        }
        #endregion

        #region Properties
        public Guid Id { get; set; }           
        public string Cliente { get; set; }     
        public string Produto { get; set; }     
        public decimal Valor { get; set; }      
        public string Status { get; set; }      
        public DateTime DataCriacao { get; set; }
        #endregion
    }
}
