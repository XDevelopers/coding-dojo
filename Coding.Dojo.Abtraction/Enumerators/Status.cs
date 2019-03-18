using System.ComponentModel;

namespace Coding.Dojo.Abtraction.Enumerators
{
    public enum Status
    {
        [Description("Inativo")]
        Indefinido = 0,

        [Description("Ativo")]
        PreCadastrado = 1
    }
}