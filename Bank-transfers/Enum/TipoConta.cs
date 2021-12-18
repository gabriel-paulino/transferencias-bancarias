using System.ComponentModel.DataAnnotations;

namespace Bank.Transfers.Enum
{
    public enum TipoConta
    {
        [Display(Name = "Pessoa fisica")]
        PessoaFisica = 1,
        [Display(Name = "Pessoa juridica")]
        PessoaJuridica = 2
    }
}