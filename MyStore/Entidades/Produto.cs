using System;

namespace MyStore.Entidades
{
    public abstract class Produto
    {
        private const decimal Desconto = 0.15m;

        public Produto(string nome,
                        string marca,
                        decimal valor)
        {
            Id = Guid.NewGuid();

            Nome = nome;
            Marca = marca;
            Valor = valor;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public decimal Valor { get; set; }

        public virtual void CalcularPreco()
        {
            var taxa = Valor * Desconto;
            Valor -= taxa;
        }
    }
}
