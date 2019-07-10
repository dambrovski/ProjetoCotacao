using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetoCotacao2.Models
{
    [Table("Estabelecimentos")]
    public class Estabelecimento
    {
        [Key]
        public int idEstabelecimento { get; set; }

        [Display(Name = "Nome: ")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string nome { get; set; }


        [Display(Name = "Telefone: ")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string telefone { get; set; }


        [Display(Name = "CNPJ: ")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string cnpj { get; set; }


        [Display(Name = "Cidade: ")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string cidade { get; set; }

        [Display(Name = "Endereco: ")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string endereco { get; set; }

        public UsuarioCliente usuariocliente { get; set; }

        public List<Alimento> alimentos { get; set; }

    }
}