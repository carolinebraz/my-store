using System;

namespace MyStore.Entidades
{
    public abstract class Pagamento
    {
        public Pagamento(string cpf,
                         decimal valor)
        {
            CodigoBarra = Guid.NewGuid();
            Cpf = cpf;
            Valor = valor;
        }

        public Guid CodigoBarra { get; set; }
        public DateTime DataPagamento { get; set; }
        public bool Confirmacao { get; set; }
        public decimal Valor { get; set; }
        public string Cpf { get; set; }

        public virtual void Pagar()
        {
            DataPagamento = DateTime.Now;
            Confirmacao = true;
        }       
    }
}
