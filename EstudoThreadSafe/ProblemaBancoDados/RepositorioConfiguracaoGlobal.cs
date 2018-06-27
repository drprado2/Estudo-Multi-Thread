using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace EstudoThreadSafe.ProblemaBancoDados
{
    public class RepositorioConfiguracaoGlobal
    {
        // Vamos usar uma lista única para representar o banco de dados
        private static IList<ConfiguracaoGlobal> _configuracoes = new List<ConfiguracaoGlobal>();

        // Utilizaremos um random para simular tempo de resposta diferentes entre os I/Os, para tentar deixar mais próximo do real
        private Random _randomTime = new Random();

        public void Adicionar(ConfiguracaoGlobal configuracao)
        {
            var tempoDemorara = _randomTime.Next(100, 900);
            Thread.Sleep(tempoDemorara);
            _configuracoes.Add(configuracao);
        }

        public IList<ConfiguracaoGlobal> Obter()
        {
            var tempoDemorara = _randomTime.Next(200, 900);
            Thread.Sleep(tempoDemorara);
            return _configuracoes.ToList();
        }

        // Método limpar para poder executar o cenário novamente
        public void Deletar()
        {
            _configuracoes.Clear();
        }
    }
}
