using Bank.Transfers.Enum;
using System;

namespace Bank.Transfers.Models
{
    public class Conta
    {
        public int Id { get; set; }
        public TipoConta TipoConta { get; set; }
        public double Saldo { get; set; }
        public double Credito { get; set; }
        public string Nome { get; set; }

        public Conta() { }

        public (bool operacao, string mensagem) Sacar(double valorSaque)
        {
            if (Saldo - valorSaque < (Credito * -1))
                return (false, "Saldo insuficiente!");

            Saldo -= valorSaque;

            return (true, $"Saldo atual da conta de {Nome} é {Saldo}");
        }

        public string Depositar(double valorDeposito)
        {
            Saldo += valorDeposito;
            return $"Saldo atual da conta de {Nome} é {Saldo}";
        }

        public void Transferir(double valorTransferencia, Conta contaDestino)
        {
            var (operacao, _) = Sacar(valorTransferencia);

            if (operacao)
                contaDestino.Depositar(valorTransferencia);
        }
    }
}