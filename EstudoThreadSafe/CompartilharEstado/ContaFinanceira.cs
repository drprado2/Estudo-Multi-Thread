using System;
using System.Collections.Generic;
using System.Text;

namespace EstudoThreadSafe.CompartilharEstado
{
    public class ContaFinanceira
    {
        private int _saldo;

        // Necessitamos de um objeto que será marcado com o bloqueio para que quando outra thread quiser executar o mesmo código
        // ela aguarde esse objeto ser desbloqueado, e quando a mesma iniciar a execução ela bloquea.
        // Isso gera um ambiente de concorrência, impactando na performance já que as threads ficam em uma fila e não se beneficiam
        // do paralelismo. Porém você eterá o benefício do "Thread Safe".
        private Object _bloquearObjeto = new Object();

        public ContaFinanceira(int saldoInicial)
        {
            _saldo = saldoInicial;
        }

        public int Retirar(int quantia)
        {
            // Rode com as linhas do lock comentadas, você verá que sempre terá o problema do saldo negativo
            // retire os comentários e você verá que o problema acaba.

            //lock (_bloquearObjeto)
            //{
                if (_saldo < 0)
                    return 0;

                if (_saldo >= quantia)
                {
                    Console.WriteLine($"Iniciando retirada de saldo, saldo atual: {_saldo} | valor a retirar: {quantia}");
                    _saldo -= quantia;
                    Console.WriteLine($"Saldo após a retirada: {_saldo}");
                    return quantia;
                }

                if (_saldo < 0)
                {
                    Console.WriteLine($"ATENÇÃO!!! Saldo negativou valor: {_saldo}.");
                    return 0;
                }

                return 0;
            //}
        }
    }
}
