using ProjetoCotacao2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjetoCotacao2.DAL
{
    public class UsuarioClienteDAO
    {
        private static Context ctx = Singleton.Instance.Context;

        public static UsuarioCliente BuscarClienteporEmail(UsuarioCliente usuario)
        {
            try
            {
                UsuarioCliente us = ctx.UsuarioClientes.FirstOrDefault(x => x.Email_Cliente.Equals(usuario.Email_Cliente));
                return us;
            }
            catch
            {
                return null;
            }
        }


        public static bool CadastrarUsuario(UsuarioCliente usuario)
        {
            try
            {
                ctx.UsuarioClientes.Add(usuario);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static void RemoverUsuario(UsuarioCliente e)
        {
            ctx.UsuarioClientes.Remove(e);
            ctx.SaveChanges();
        }


        public static UsuarioCliente BuscarUsuarioPorEmailSenha(UsuarioCliente usuario)
        {
            return ctx.UsuarioClientes.
                FirstOrDefault(x => x.Email_Cliente.Equals(usuario.Email_Cliente) &&
                x.SenhaCliente.Equals(usuario.SenhaCliente));
        }


        public static UsuarioCliente BuscarClienteporID(int? idusuario)
        {
            return ctx.UsuarioClientes.Find(idusuario);
        }


        public static UsuarioCliente BuscarClienteporEmail(string Email_Cliente)
        {
            return ctx.UsuarioClientes.FirstOrDefault(x => x.Email_Cliente.Equals(Email_Cliente));
        }

        public static void Limpar()
        {
            ctx.AlimentosCotacao.RemoveRange(ctx.AlimentosCotacao);
            ctx.SaveChanges();
        }


        public static bool EditarCliente(UsuarioCliente usuarioCliente)
        {
            try
            {
                var usuariodoBanco = UsuarioClienteDAO.BuscarClienteporID(usuarioCliente.IDUsuarioCliente);
                usuariodoBanco.Nome_Cliente = usuarioCliente.Nome_Cliente;
                usuariodoBanco.Sobrenome = usuarioCliente.Sobrenome;
                usuariodoBanco.ConfirmacaoSenha = usuarioCliente.ConfirmacaoSenha;
                usuariodoBanco.SenhaCliente = usuarioCliente.SenhaCliente;
                usuariodoBanco.CPF_Cliente = usuarioCliente.CPF_Cliente;
                usuariodoBanco.Email_Cliente = usuarioCliente.Email_Cliente;
                usuariodoBanco.Telefone_Cliente = usuarioCliente.Telefone_Cliente;
                ctx.Entry(usuariodoBanco).State = EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }


    }
}