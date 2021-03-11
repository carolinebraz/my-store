namespace MyStore.Entidades
{
    class Televisao : Produto
    {
        public Televisao(string nome,
                      string marca,
                      decimal valor,
                      string smart,
                      string led,
                      int polegadas)
          : base(nome, marca, valor)
        {
            SmartTV = smart;
            Led = led;
            Polegadas = polegadas;
        }
        public int Polegadas { get; set; }
        public string Led { get; set; }
        public string SmartTV { get; set; }
    }
}
