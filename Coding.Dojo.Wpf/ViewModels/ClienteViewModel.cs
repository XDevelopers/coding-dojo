using Coding.Dojo.Abtraction;
using Coding.Dojo.Abtraction.Enumerators;
using System;

namespace Coding.Dojo.Wpf.ViewModels
{
    public class ClienteViewModel : Cliente
    {
        public ClienteViewModel(Cliente cliente)
        {
            if (cliente.Endereco != null)
            {
                Endereco = cliente.Endereco.ToString();
            }

            NomeCompleto = cliente.NomeCompleto;
            Cpf = cliente.Cpf;
            DataNascimento = cliente.DataNascimento;
            Email = cliente.Email;
            TelefoneCelular = cliente.TelefoneCelular;
            DataCadastro = DateTime.Now;
            Status = cliente.Status;
        }

        public new string NomeCompleto { get; set; }

        public new string Cpf { get; set; }

        public new DateTime DataNascimento { get; set; }

        public new string Email { get; set; }

        public new string Endereco { get; set; }

        public new string TelefoneCelular { get; set; }

        public new DateTime DataCadastro { get; set; }

        public new Status Status { get; set; }

        public new bool ConsideradoAdulto { get; set; }
    }
}