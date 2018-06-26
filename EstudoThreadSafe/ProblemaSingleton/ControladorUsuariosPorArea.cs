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

        // Como sabemos do problema do controle de estado já criamos a variável para sincronizar o método que atualiza 
        private Object _bloquearInstancia = new Object();
        private static Object _bloqueioSingleton = new Object();

        private ControladorUsuariosPorArea()
        {
            _numeroUsuariosNaArea = 0;
            _numeroMaximoUsuariosPermitidos = 3;
            _usuariosNaArea = new List<string>();
        }

        public static ControladorUsuariosPorArea ObterInstancia()
        {
            if (_instanciaUnica == null)
            {
                lock (_bloqueioSingleton)
                {
                    if(_instanciaUnica == null)
                        _instanciaUnica = new ControladorUsuariosPorArea();
                }
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
