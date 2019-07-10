using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetoCotacao2.Models
{
    [Table("UsuarioClientes")]
    public class UsuarioCliente
    {

        [Key]
        public int IDUsuarioCliente { get; set; }

        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "E-mail inválido!")]
        public string Email_Cliente { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Login do usuário")]
        public string Login { get; set; }




        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string SenhaCliente { get; set; }


        [Display(Name = "Confirmação Senha")]
        [Compare("SenhaCliente", ErrorMessage = "Os campos não coincidem!")]
        [DataType(DataType.Password)]
        [NotMapped]

        public string ConfirmacaoSenha { get; set; }

        [Display(Name = "CPF")]

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string CPF_Cliente { get; set; }

        [Display(Name = "Nome")]
        [MinLength(3, ErrorMessage = " No mínimo 3 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Nome_Cliente { get; set; }


        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Sobrenome")]
        public string Sobrenome { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Telefone_Cliente { get; set; }


        public string id { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        

    }
}