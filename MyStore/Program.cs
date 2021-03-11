using MyStore.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyStore
{
    class Program
    {
        private static List<Dinheiro> listaDinheiro;
        private static List<Boleto> listaBoletos;
        private static List<Produto> listaProdutos;

        static void Main(string[] args)
        {
            listaDinheiro = new List<Dinheiro>();
            listaBoletos = new List<Boleto>();
            listaProdutos = new List<Produto>();

           while (true)
            {
                Console.WriteLine("\n================================================================");
                Console.WriteLine("========================= Loja da Carol ========================");
                Console.WriteLine("================================================================\n");
                Console.WriteLine("Selecione a opção desejada: ");
                Console.WriteLine(" 1 - Compra | 2 - Pagamento | 3 - Relatório | 4 - Sair da loja");

                var opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        Comprar();
                        break;
                    case 2:
                        Pagar();
                        break;
                    case 3:
                        Relatorio();
                        break;
                    case 4:
                        return;
                    default:
                        break;
                }
            }
        }
        public static void Comprar()
        {
            var tv = new Televisao("TV", "LG", 3000, "SmartTV", "LED", 55);
            tv.CalcularPreco();
            
            var refrigerador = new Geladeira("Refrigerador", "Brastemp", 4000, "FrostFree", 500, "inox");
            refrigerador.CalcularPreco();

            Console.WriteLine("====================== OFERTAS IMPERDÍVEIS =====================\n");
            Console.WriteLine($" {tv.Nome} {tv.Marca} {tv.Polegadas}\" {tv.SmartTV} R$ {tv.Valor}");
            Console.WriteLine($" {refrigerador.Nome} {refrigerador.Marca} {refrigerador.FrostFree}" +
                              $" {refrigerador.Litros}L {refrigerador.Cor} R$ {refrigerador.Valor}");
            Console.WriteLine("\n================================================================\n");

            Console.WriteLine("\nDigite o valor do produto escolhido: ");
            var valor = Decimal.Parse(Console.ReadLine());

            if (valor == tv.Valor)
            {
                listaProdutos.Add(tv);
            }
            if (valor == refrigerador.Valor)
            {
                listaProdutos.Add(refrigerador);
            }
            if (valor != tv.Valor && valor != refrigerador.Valor)
            {
                Console.WriteLine("\n Por gentileza, digite um valor válido.");
                return;
            }

            Console.WriteLine("\nDigite o CPF do cliente: ");
            var cpf = Console.ReadLine();

            Console.WriteLine("\nDigite uma descrição caso necessário: ");
            var descricao = Console.ReadLine();

            Console.WriteLine("\nDigite a forma de pagamento: " +
                              "\n 1 - À vista | 2 - Boleto | 0 - Cancelar compra ");
            var pagamento = int.Parse(Console.ReadLine());

            switch (pagamento)
            {
                case 0:
                    Console.WriteLine("Sua compra foi cancelada");
                    break;

                case 1:
                    var dinheiro = new Dinheiro(cpf, valor);
                    dinheiro.CalcularDesconto();
                    
                    Console.WriteLine($"\n Compra realizada com sucesso!" +
                                      $"\n Valor: R$ {valor}" +
                                      $"\n Valor a ser pago: R$ {dinheiro.Valor} [com desconto de 10%] " +
                                      $"\n Data da compra: {dinheiro.DataCompra}" +
                                      $"\n\nPara efetuar o pagamento de sua compra " +
                                      $"selecione a opção 2");

                    listaDinheiro.Add(dinheiro);
                    break;

                case 2:
                    var boleto = new Boleto(cpf, valor, descricao);
                    boleto.GerarBoleto();

                    Console.WriteLine($"\n Boleto gerado com sucesso!" +
                                      $"\n Valor do boleto: R$ {boleto.Valor}" +
                                      $"\n Código de barras: {boleto.CodigoBarra} " +
                                      $"\n Data de vencimento: {boleto.DataVencimento}" +
                                      $"\n\nPara efetuar o pagamento de seu boleto " +
                                      $"selecione a opção 2");

                    listaBoletos.Add(boleto);
                    break;

                default:
                    break;
            }
        }

        public static void Pagar()
        {
            Console.WriteLine("Digite a forma de pagamento: " +
                                          "\n 1 - À vista | 2 - Boleto ");
            var pagamento = int.Parse(Console.ReadLine());
            if (pagamento == 1)
            {
                Console.WriteLine("Digite o valor da compra que deseja pagar\n R$ ");
                var numero = Convert.ToDecimal(Console.ReadLine());
                var dinheiro = listaDinheiro
                                    .Where(item => item.Valor == numero)
                                    .FirstOrDefault();

                if (dinheiro is null)
                {
                    Console.WriteLine($"\n Compra de valor R$ {numero} não encontrada!" +
                                      $"\n Por favor, verifique o valor digitado e tente novamente");
                    return;
                }

                dinheiro.Pagar();

                Console.WriteLine($"\n Pagamento à vista efetuado com sucesso! " +
                                  $"\n Valor R$ {dinheiro.Valor}");
            }
            if (pagamento == 2)
            {
                Console.WriteLine("Digite o código de barras: ");
                var numero = Guid.Parse(Console.ReadLine());

                var boleto = listaBoletos
                                    .Where(item => item.CodigoBarra == numero)
                                    .FirstOrDefault();
                if (boleto is null)
                {
                    Console.WriteLine($"\nBoleto de código {numero} não encontrado!");
                    return;
                }

                if (boleto.EstaPago())
                {
                    Console.WriteLine($"\nO boleto foi pago no dia : {boleto.DataPagamento}");
                    return;
                }

                if (boleto.DataVencimento < DateTime.Now)
                {
                    boleto.CalcularJuros();
                    Console.WriteLine($"\n O boleto está vencido e será acrescido 10%" +
                                      $"\n de multa sobre o valor === R$ {boleto.Valor}");
                }

                boleto.Pagar();

                Console.WriteLine($"\n Pagamento de boleto efetuado com sucesso!" +
                                  $"\n Valor R$ {boleto.Valor}");
            }
        }      

        public static void Relatorio()
        {
            Console.WriteLine("Qual opção de relatório? " +
                              "\n 1 - Compras à vista | 2 - Compras com boleto | 3 - Produtos");

            var opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
            {
                Console.WriteLine("Você gostaria de visualizar:" +
                                  "\n 1 - Compras pagas | 2 - Compras a pagar");
                var opcao1 = int.Parse(Console.ReadLine());
                switch (opcao1)
                {
                case 1:
                    DinheiroPago();
                    break;
                case 2:
                    DinheiroAPagar();
                    break;
                default:
                    break;
                }
            }

            if (opcao == 2)
            {
                Boletos();
            }

            if (opcao == 3)
            {
                var produtos = listaProdutos
                                       .ToList();

                foreach (var item in produtos)
                {
                    Console.WriteLine($"\n Produto: {item.Nome} {item.Marca}" +
                                      $"\n Valor: RS {item.Valor}");
                }
            }
        }

        public static void DinheiroPago()
        {
            var dinheiro = listaDinheiro
                .Where(item => item.Confirmacao)
                .ToList();

            foreach (var item in dinheiro)
            {
                Console.WriteLine($"\n Valor pago: R$ {item.Valor}" +
                                  $"\n Data de pagamento: {item.DataPagamento}");
            }
        }

        public static void DinheiroAPagar()
        {
            var dinheiro = listaDinheiro
                .Where(item => item.Confirmacao == false)
                .ToList();

            foreach (var item in dinheiro)
            {
                Console.WriteLine($"\n Valor a ser pago: R$ {item.Valor}" +
                                  $"\n Data da compra: {item.DataCompra}");
            }
        }
        public static void Boletos()
        {
            Console.WriteLine("\n======================== Boletos pagos =========================\n");

            var boletosPagos = listaBoletos
                .Where(item => item.Confirmacao)
                .ToList();

            foreach (var item in boletosPagos)
            {
                Console.WriteLine($"\n Código de barras: {item.CodigoBarra} " +
                                  $"\n Valor pago: R$ {item.Valor}" +
                                  $"\n Data de pagamento: {item.DataPagamento}");
            }

            Console.WriteLine("\n======================= Boletos a pagar ========================\n");
            var boletosAPagar = listaBoletos
                .Where(item => item.Confirmacao == false
                && item.DataVencimento > DateTime.Now)
                .ToList();

            foreach (var item in boletosAPagar)
            {
                Console.WriteLine($"\n Código de barras: {item.CodigoBarra} " +
                                  $"\n Valor a ser pago: R$ {item.Valor}" +
                                  $"\n Data de vencimento: {item.DataVencimento}");
            }

            Console.WriteLine("\n====================== Boletos vencidos ========================\n");
            var boletosVencidos = listaBoletos
                .Where(item => item.Confirmacao == false
                && item.DataVencimento < DateTime.Now)
                .ToList();

            foreach (var item in boletosVencidos)
            {
                Console.WriteLine($"\n Código de barras: {item.CodigoBarra} " +
                                  $"\n Valor: R$ {item.Valor}" +
                                  $"\n Data de vencimento: {item.DataVencimento}");
            }
        }
    }
}
