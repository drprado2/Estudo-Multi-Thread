using System;
using System.Collections.Generic;
using System.Text;

namespace EstudoThreadSafe.ProblemaBancoDados
{
    // Entidade que mante estado mas devia possuir apenas 1 registro
    public class ConfiguracaoGlobal
    {
        private Guid _id;
        private bool _podeFazerAlgo;

        public Guid Id
        {
            get => _id;
        }

        public bool PodeFazerAlgo
        {
            get => _podeFazerAlgo;
        }

        public ConfiguracaoGlobal(bool podeFazerAlgo)
        {
            _id = Guid.NewGuid();
            _podeFazerAlgo = podeFazerAlgo;
        }
    }
}
