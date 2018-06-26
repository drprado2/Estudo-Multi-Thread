using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EstudoThreadSafe.ProblemaSingleton
{
    public class Exemplo
    {
        public void Executar()
        {
            var threads = new Thread[20];

            for (int i = 0; i < threads.Length; i++)
            {
                var i1 = i;
                threads[i] = new Thread(new ThreadStart(() => ControladorUsuariosPorArea.ObterInstancia().TentarAdicionarUsuarioArea($"João {i1}")));
            }

            foreach (var t in threads)
                t.Start();

            foreach (var t in threads)
                t.Join();

            ControladorUsuariosPorArea.ObterInstancia().ImprimirUsuarioNaArea();
        }
    }
}
