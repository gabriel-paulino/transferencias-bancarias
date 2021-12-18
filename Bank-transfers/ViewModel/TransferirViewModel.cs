using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bank.Transfers.ViewModel
{
    public class TransferirViewModel
    {
        public SelectList Contas { get; set; }
        public int ContaOrigemId { get; set; }
        public int ContaDestinoId { get; set; }
        public double ValorTransferencia { get; set; }

    }
}