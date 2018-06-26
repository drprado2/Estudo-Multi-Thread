using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EstudoThreadSafe.CompartilharEstado
{
    public class Exemplo
    {
        private Random _random = new Random();

        // Iremos simular esse método para tentar causar o problema do saldo negativo
        // quem olhar o código fonte e ver o if que temos antes de movimentar pode pensar que nunca gerará
        // o problema. Porém em aplicações Web API usando Kestrel e IIS a aplicação será multi thread
        // Criando 1 nova thread para atender cada request, e nesse cenário de paralelismo veremos o problema ocorrer.
        private void SimularMovimentacoes(ContaFinanceira conta)
        {
            for (int i = 0; i < 100; i++)
            {
                var valorMovimentar = _random.Next(1, 100);
                conta.Retirar(valorMovimentar);
            }
        }

        public void Executar()
        {
            // O grande problema é que só temos 1 instancia, e essa possui estado, a instancia
            // compartilhada entre as threads e com estado é o que gerará o problema
            var conta = new ContaFinanceira(100);

            // Criaremos 10 threads, o que seria equivalente a 10 requests simultâneas em uma
            // aplicação web API
            var threads = new Thread[10];

            // Colocamos cada thread para executar as movimentação
            for (int i = 0; i < threads.Length; i++)
                threads[i] = new Thread(new ThreadStart(() => SimularMovimentacoes(conta)));

            // Iniciamos a execução das threads
            foreach (var t in threads)
                t.Start();

            // Pausamos a thread principal aguardando as demais
            foreach (var t in threads)
                t.Join();
        }
    }
}
