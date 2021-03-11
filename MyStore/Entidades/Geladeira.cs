namespace MyStore.Entidades
{
    public class Geladeira : Produto
    {
        private const decimal DescontoGerente = 100;

        public Geladeira(string nome,
                        string marca,
                        decimal valor,
                        string frostfree,
                        int litros,
                        string cor)
            : base(nome, marca, valor)
        {
            FrostFree = frostfree;
            Litros = litros;
            Cor = cor;
        }
        public string FrostFree { get; set; }
        public int Litros { get; set; }
        public string Cor { get; set; }

        public override void CalcularPreco()
        {
            base.CalcularPreco();
            Valor -= DescontoGerente;
        }
    }
}
