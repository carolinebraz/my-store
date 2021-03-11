using System;

namespace MyStore.Entidades
{
    class Dinheiro : Pagamento
    {
        private const decimal Desconto = 0.10m;

        public Dinheiro(string cpf,
                        decimal valor)
            : base (cpf, valor)
        {
            DataCompra = DateTime.Now;
        }

        public DateTime DataCompra { get; set; }

        public void CalcularDesconto()
        {
            var taxa = Valor * Desconto;
            Valor -= taxa;
        }
    }
}
