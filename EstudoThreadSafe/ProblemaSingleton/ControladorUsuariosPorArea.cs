using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EstudoThreadSafe.ProblemaSingleton
{
    // Imagine o cenário onde temos uma área física e queremos limitar o número de usuário simultâneos na área
    // teremos uma API que irá receber uma requisição dizendo que um usuário que entrar na área, se a área suportar
    // o usuário será adicionado e já retornará um 200 para informar que deu certo.
    // Se a área estiver lotada, não adicionamos o usuário e retornamos um erro.

    // Como sabemos que a API é multi thread, e criará 1 thread para cada request, e que as requests chegam em paralelo
    // podendo chegar ao mesmo tempo, criamos esse controlador de usuários por área como um singleton, para evitar o problema
    public class ControladorUsuariosPorArea
    {
        private static ControladorUsuariosPorArea _instanciaUnica;

        private int _numeroUsuariosNaArea;
        private int _numeroMaximoUsuariosPermitidos;
        private IList<string> _usuariosNaArea;

        // Como sabemos do problema do controle de estado já criamos a variável para sincronizar o método que atualiza o estado
        private Object _bloquearInstancia = new Object();

        // Aqui é parte da solução, devemos ter uma maneira de bloquear a primeira criação do singleton tmb, pois devemos saber que
        // a criação inicial pode ocorrer em paralelo, e teremos o mesmo problema do estado.
        // Nesse caso a variável de bloqueio deve ser estático pela maneira como o singleton é implementado.
        private static Object _bloqueioSingleton = new Object();

        private ControladorUsuariosPorArea()
        {
            _numeroUsuariosNaArea = 0;
            _numeroMaximoUsuariosPermitidos = 3;
            _usuariosNaArea = new List<string>();
        }

        public static ControladorUsuariosPorArea ObterInstancia()
        {
            // Aqui vem o segundo segredo da implementação, o Singleton tem o benefício de carregamento preguiçoso,
            // ou seja diferente das variáveis globais o singleton só cria a instancia quando é solicitado a primeira vez
            // só que em aplicações multi thread a solicitação da instancia inicial pode ocorrer em paralelo.
            // Então sem blocar o código verificamos se a instancia ainda está nula. Depois da 1 criação nunca vai entrar
            // nesse IF, mas sabemos que a primeira criação pode estar ocorrendo em paralelo.
            if (_instanciaUnica == null)
            {
                // Após entrar no if ai sim bloqueamos o objeto, para garantir a concorrência nesse trecho do código, ao invéz do paralelismo

                // ATENÇÃO!! RETIRE OS COMENTÁRIOS DA LINHA ABAIXO PARA VER O PROBLEMA SER RESOLVIDO
                //lock (_bloqueioSingleton)
                //{
                    // Aqui fazemos a mesma verificação de novo, ai mesmo que passamos do primeiro if no caso da criação inicial paralela
                    // não passaremos desse segundo, e não geraremos o problema
                    if (_instanciaUnica == null)
                        _instanciaUnica = new ControladorUsuariosPorArea();
                //}
            }

            return _instanciaUnica;
        }

        public bool TentarAdicionarUsuarioArea(string usuario)
        {
            lock (_bloquearInstancia)
            {
                Console.WriteLine($"Tentando adicionar usuário {usuario} a área, número atual de usuários: {_numeroUsuariosNaArea}");
                if (_numeroUsuariosNaArea == _numeroMaximoUsuariosPermitidos)
                    return false;

                _numeroUsuariosNaArea++;
                _usuariosNaArea.Add(usuario);

                Console.WriteLine($"Usuário {usuario} foi adicionado a área, número atual de usuários na área: {_numeroUsuariosNaArea}");
                return true;
            }
        }

        public void ImprimirUsuarioNaArea()
        {
            var usuariosNaArea = new StringBuilder();

            foreach (var u in _usuariosNaArea)
                usuariosNaArea.AppendLine(u);

            Console.WriteLine($"Usuários na área: {usuariosNaArea.ToString()}");
        }
    }
}
