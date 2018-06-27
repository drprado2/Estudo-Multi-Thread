using System;

namespace EstudoThreadSafe
{
    class Program
    {
        static void ExecutarExemplos()
        {
            Console.Clear();
            Console.WriteLine("Escolha uma das opções");
            Console.WriteLine("==============================================");
            Console.WriteLine("1 - Exemplo compartilhamento de estado");
            Console.WriteLine("2 - Exemplo problema com singleton");
            Console.WriteLine("3 - Problema entidade de registro único");

            var escolha = Console.ReadKey();
            Console.WriteLine("\n\n");
            var convertida = Convert.ToUInt32(escolha.KeyChar.ToString());

            switch (convertida)
            {
                case (1):
                {
                    var exemplo = new CompartilharEstado.Exemplo();
                    exemplo.Executar();
                    Console.WriteLine("\n******* FIM DO EXEMPLO *******");
                    break;
                }
                case (2):
                {
                    var exemplo = new ProblemaSingleton.Exemplo();
                    exemplo.Executar();
                    Console.WriteLine("\n******* FIM DO EXEMPLO *******");
                    break;
                }
                case (3):
                {
                    var exemplo = new ProblemaBancoDados.Exemplo();
                    exemplo.Executar();
                    Console.WriteLine("\n******* FIM DO EXEMPLO *******");
                    break;
                }
                default:
                {
                    Console.WriteLine("Opção inválida, selecione novamente");
                    break;
                }
            }

            Console.WriteLine("\nAperte qualquer tecla para iniciar novamente");
            Console.ReadKey();
            ExecutarExemplos();
        }

        static void Main(string[] args)
        {
            ExecutarExemplos();
        }
    }
}
