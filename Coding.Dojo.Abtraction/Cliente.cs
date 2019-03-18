using Coding.Dojo.Abtraction.Enumerators;
using System;

namespace Coding.Dojo.Abtraction
{
    /// <summary>
    /// https://my.api.mockaroo.com/codingdojo.json?key=beda91c0
    /// </summary>
    public class Cliente
    {
        public string NomeCompleto { get; set; }

        public string Cpf { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Email { get; set; }

        public Endereco Endereco { get; set; }

        public string TelefoneCelular { get; set; }

        public DateTime DataCadastro { get; set; }

        public Status Status { get; set; }

        public bool ConsideradoAdulto { get; set; }
    }
}