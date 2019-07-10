using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetoCotacao2.Models
{
    [Table("Alimentos")]
    public class Alimento
    {


        [Key]
        public int idAlimento { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string descricao { get; set; }

        public Categoria Categoria { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string nome { get; set; }

        [Display(Name = "Preço")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public double preco { get; set; }

        [Display(Name = "Unidade")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string unidade { get; set; }

        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public int quantidade { get; set; }

        public Estabelecimento estabelecimento { get; set; }

    }
}