using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EstudoThreadSafe.ProblemaBancoDados
{
    // CENÁRIO: Temos 1 entidade de configuração global em nosso sistema, e só queremos que haja 1 registro dessa entidade
    // ou seja na tabela do nosso banco sempre terá 1 linha só para esse registro.
    public class Exemplo
    {
        public void Executar()
        {
            // Iremos simular 20 threads simultâneas, o equivalente a 20 pessoas diferentes abrirem a tela que cria
            // a configuração global, e as 20 apertarem no salvar em paralelo
            var threads = new Thread[20];

            var repo = new RepositorioConfiguracaoGlobal();
            // Limpamos as configurações existentes, para poder rodar o exemplo várias vezes
            repo.Deletar();

            for (int i = 0; i < threads.Length; i++)
            {
                var i1 = i;
                // Cada thread basicamente irá tentar criar uma nova configuração, o que seria feito clicando no botão salvar da tela
                threads[i] = new Thread(new ThreadStart(() => new ServicoConfiguracaoGlobal().Criar(new ConfiguracaoGlobalDto(){PodeFazerAlgo = i1 % 2 == 0})));
            }

            // Iniciar execução das threads em paralelo
            foreach (var thread in threads)
                thread.Start();

            // Pausar thread principal aguardando o processamento das demais
            foreach (var thread in threads)
                thread.Join();

            var qtdConfigsCriadas = repo.Obter().Count;

            Console.WriteLine($"Total de configurações criadas após o exemplo {qtdConfigsCriadas}");
        }
    }
}
