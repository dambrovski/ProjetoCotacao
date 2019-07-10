using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetoCotacao2.Models
{
    [Table("AlimentoCotacao")]
    public class AlimentoCotacao
    {
        [Key]
        public int AlimentoCotacaoId { get; set; }
        public Alimento alimento { get; set; }
        public int Quantidade { get; set; }
        public double Preco { get; set; }
        public string CarrinhoId { get; set; }
    }
}