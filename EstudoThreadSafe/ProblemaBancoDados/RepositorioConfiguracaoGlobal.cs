using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EstudoThreadSafe.ProblemaBancoDados
{
    public class RepositorioConfiguracaoGlobal
    {
        private IList<ConfiguracaoGlobal> _configuracoes;

        public RepositorioConfiguracaoGlobal()
        {
            _configuracoes = new List<ConfiguracaoGlobal>();
        }

        public void Adicionar(ConfiguracaoGlobal configuracao)
        {
            Thread.Sleep(100);
            _configuracoes.Add(configuracao);
        }
    }
}
