using System.Diagnostics;
using System.Globalization;

namespace Program
{
    public class Dummy
    {
        public Guid Id = Guid.NewGuid();
        public DateTime CreationDate = DateTime.Now;

        public Dummy()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Dummy: {Id} | Criado às: {CreationDate.ToString(CultureInfo.GetCultureInfo("pt-br"))}";
        }
    }
    public class Program
    {
        public delegate IEnumerable<Dummy> TimeItMethod();
        public static readonly int MAX_ITEM_COUNT = 10;
        public static readonly bool SHOW_GENERATED_ITEMS = true;
        public static readonly bool SIMULATE_DELAY = true;
        public static readonly int DELAY_TIME = 1000;

        public static IEnumerable<Dummy> WithIterator()
        {
            for (int i = 0; i < MAX_ITEM_COUNT; i++)
            {
                if (SIMULATE_DELAY)
                    Thread.Sleep(DELAY_TIME);
                yield return new Dummy();
            }
        }

        public static IEnumerable<Dummy> WithoutIterator()
        {
            List<Dummy> result = new();
            for (int i = 0; i < MAX_ITEM_COUNT; i++)
            {
                if (SIMULATE_DELAY)
                    Thread.Sleep(DELAY_TIME);
                result.Add(new Dummy());
            }
            return result;
        }

        public static void TimeLog(object x)
        {
            Console.WriteLine($"[{DateTime.Now.ToString(CultureInfo.GetCultureInfo("pt-br"))}]: {x}");
        }

        public static void TimeIt(TimeItMethod method)
        {
            Stopwatch stopwatch = new();
            TimeLog($"Iniciando o método: '{method.Method.Name}'.");
            stopwatch.Start();
            foreach (Dummy item in method.Invoke())
            {
                if (SHOW_GENERATED_ITEMS)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            stopwatch.Stop();
            TimeLog($"Finalizado o método '{method.Method.Name}' após {stopwatch.Elapsed.Seconds} segundos.");
        }

        public static void Main(string[] args)
        {
            Console.WriteLine($"===== Gerando {MAX_ITEM_COUNT} itens =====");

            TimeIt(WithoutIterator);
            TimeIt(WithIterator);
        }
    }
}