using System.Diagnostics;
using Program.Models;

namespace Program.Utils
{
    public static class Time
    {
        public delegate IEnumerable<Dummy> Method();

        /// <summary>
        /// Escreve no console uma mensagem com a hora em que foi escrita na frente
        /// </summary>
        public static void Log(object x)
        {
            Console.WriteLine($"[{DateTime.Now}]: {x}.");
        } 

        /// <summary>
        /// Mede o tempo em que o método informado demora para executar
        /// </summary>
        /// <param name="method"></param>
        /// <param name="displayItems"></param>
        public static void Measure(Method method, bool displayItems = false)
        {
            Stopwatch stopwatch = new();
            Log($"Iniciando o método: '{method.Method.Name}'");
            stopwatch.Start();
            foreach (Dummy item in method.Invoke())
            {
                if (displayItems)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            stopwatch.Stop();
            Log($"Finalizado o método '{method.Method.Name}' após {stopwatch.Elapsed}");
        }
    }
}