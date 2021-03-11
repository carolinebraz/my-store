using System;

namespace MyStore.Entidades
{
    public class Boleto : Pagamento
    {
        private const int DiasVencimento = 15;
        private const decimal Juros = 0.10m;

        public Boleto(  string cpf, 
                        decimal valor, 
                        string descricao)
            : base(cpf, valor)
        {
            Descricao = descricao;
            DataEmissao = DateTime.Now;
        }

        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public string Descricao { get; set; }

        public void GerarBoleto()
        {
            CodigoBarra = Guid.NewGuid();
            DataVencimento = DataEmissao.AddDays(DiasVencimento);
        }

        public bool EstaPago()
        {
            return Confirmacao;
        }

        public void CalcularJuros()
        {
            var taxa = Valor * Juros;
            Valor += taxa;
        }
    }
}
