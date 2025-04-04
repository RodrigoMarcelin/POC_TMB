using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.DTO
{
    public record OrderDTO
    {
        public Guid Id { get; set; }
        public string Cliente { get; set; }
        public string Produto { get; set; }
        public decimal Valor { get; set; }
        public string Status { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
