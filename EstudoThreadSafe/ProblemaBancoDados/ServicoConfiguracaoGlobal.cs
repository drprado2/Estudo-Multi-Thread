using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EstudoThreadSafe.ProblemaBancoDados
{
    // A classe de serviço é uma classe de instancia normal, sendo que para cada request obtemos uma nova instancia
    // isso poderia ser feito através dos ciclos de vida Transient ou Scoped em um framework de IoC
    public class ServicoConfiguracaoGlobal
    {
        private RepositorioConfiguracaoGlobal _repositorio;

        // Aqui está a sacada para resolver o problema, devemos fazer o bloqueio de um objeto para tornar o código de criação
        // concorrente ao invéz de paralelo.
        // Porém como a classe é uma classe de instancia se o objeto fosse uma variável de instancia também ele seria diferente 
        // entre os objetos, e cada objeto bloquearia o seu próprio, logo não adiantaria de nada.
        // Para resolver isso criamos 1 objeto único para o bloqueio, tornando ele estático e já pré inicializado
        private static Object _bloquearInstancia = new Object();

        public ServicoConfiguracaoGlobal()
        {
            _repositorio = new RepositorioConfiguracaoGlobal();
        }

        public void Criar(ConfiguracaoGlobalDto dto)
        {
            // tornamos o código de criação concorrente

            // ATENÇÃO!! REMOVA OS COMENTÁRIOS ABAIXO PARA VER O PROBLEMA SER RESOLVIDO
            //lock (_bloquearInstancia)
            //{
                Console.WriteLine("Tentando adicionar configuração");
                var configuracaoJaExiste = _repositorio.Obter().Any();

                if (configuracaoJaExiste)
                {
                    Console.WriteLine("Não será adicionada a configuração, pois já existe 1 configuração válida");
                    return;
                }

                _repositorio.Adicionar(new ConfiguracaoGlobal(dto.PodeFazerAlgo));
                Console.WriteLine("Nova configuração adicionada com sucesso");
            //}
        }
    }
}
