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
            TimeLog($"Iniciando o método: '{method.Method.Name}'");
            stopwatch.Start();
            foreach (Dummy item in method.Invoke())
            {
                if (SHOW_GENERATED_ITEMS)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            stopwatch.Stop();
            TimeLog($"Finalizado o método '{method.Method.Name}' após {TimeString(stopwatch.Elapsed)}");
        }

        public static string TimeString(TimeSpan elapsedTime)
        {
            List<string> results = new();
            string result = string.Empty;

            if (elapsedTime.Hours > 0)
            {
                if (elapsedTime.Hours > 1)
                {
                    results.Add($"{elapsedTime.Hours} horas");
                }
                else
                {
                    results.Add($"{elapsedTime.Hours} hora");
                }
            }

            if (elapsedTime.Minutes > 0)
            {
                if (elapsedTime.Minutes > 1)
                {
                    results.Add($"{elapsedTime.Minutes} minutos");
                }
                else
                {
                    results.Add($"{elapsedTime.Minutes} minuto");
                }
            }

            if (elapsedTime.Seconds > 0)
            {
                if (elapsedTime.Seconds > 1)
                {
                    results.Add($"{elapsedTime.Seconds} segundos");
                }
                else
                {
                    results.Add($"{elapsedTime.Seconds} segundo");
                }
            }

            if (elapsedTime.Milliseconds > 0)
            {
                if (elapsedTime.Milliseconds > 1)
                {
                    results.Add($"{elapsedTime.Milliseconds} milisegundos");
                }
                else
                {
                    results.Add($"{elapsedTime.Milliseconds} milisegundos");
                }
            }

            if (elapsedTime.Nanoseconds > 0)
            {
                if (elapsedTime.Nanoseconds > 1)
                {
                    results.Add($"{elapsedTime.Nanoseconds} nanosegundos");
                }
                else
                {
                    results.Add($"{elapsedTime.Nanoseconds} nanosegundos");
                }
            }

            for (int i = 0; i < results.Count; i++)
            {
                result += $"{results[i]} ";
                if (results.Count > 1 && i == results.Count - 2)
                {
                    result += "e ";
                }
            }

            return result;
        }

        public static void Main(string[] args)
        {
            Console.WriteLine($"===== Gerando {MAX_ITEM_COUNT} itens =====");

            TimeIt(WithoutIterator);
            TimeIt(WithIterator);
        }
    }
}
