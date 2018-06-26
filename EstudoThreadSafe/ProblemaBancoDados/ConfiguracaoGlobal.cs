using System;
using System.Collections.Generic;
using System.Text;

namespace EstudoThreadSafe.ProblemaBancoDados
{
    // CENÁRIO: Temos 1 entidade de configuração global em nosso sistema, e só queremos que haja 1 registro dessa entidade
    // ou seja na tabela do nosso banco sempre terá 1 linha só para esse registro.
    public class ConfiguracaoGlobal
    {
        private int _id;
        private bool _podeFazerAlgo;

        public int Id
        {
            get => _id;
        }

        public bool PodeFazerAlgo
        {
            get => _podeFazerAlgo;
        }

        public ConfiguracaoGlobal(int id, bool podeFazerAlgo)
        {
            _id = id;
            _podeFazerAlgo = podeFazerAlgo;
        }
    }
}
