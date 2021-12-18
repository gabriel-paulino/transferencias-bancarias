using Bank.Transfers.Enum;

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

        /*
public bool Sacar(double valorSaque)
{
    if (Saldo - valorSaque < (Credito * -1))
    {
        Console.WriteLine("Saldo insuficiente!");
        return false;
    }

    Saldo -= valorSaque;

    Console.WriteLine("Saldo atual da conta de {0} é {1}", Nome, Saldo);

    return true;
}

public void Depositar(double valorDeposito)
{
    Saldo += valorDeposito;

    Console.WriteLine("Saldo atual da conta de {0} é {1}", Nome, Saldo);
}

public void Transferir(double valorTransferencia, Conta contaDestino)
{
    if (Sacar(valorTransferencia))
        contaDestino.Depositar(valorTransferencia);
}

public override string ToString()
{
    string retorno = "";
    retorno += "TipoConta " + TipoConta + " | ";
    retorno += "Nome " + Nome + " | ";
    retorno += "Saldo " + Saldo + " | ";
    retorno += "Crédito " + Credito;

    return retorno;
}
*/
    }
}