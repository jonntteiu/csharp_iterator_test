using System.Globalization;
using Program.Models;
using Program.Utils;

namespace Program
{
    public static class Program
    {
        public static readonly int MAX_ITEM_COUNT = 100_000_000;
        public static readonly bool SHOW_GENERATED_ITEMS = false;
        public static readonly bool SIMULATE_DELAY = false;
        public static readonly int DELAY_TIME = 1000;

        /// <summary>
        /// Método que utiliza iteradores para retornar valores
        /// </summary>
        public static IEnumerable<Dummy> WithIterator()
        {
            for (int i = 0; i < MAX_ITEM_COUNT; i++)
            {
                if (SIMULATE_DELAY)
                    Thread.Sleep(DELAY_TIME);
                yield return new Dummy();
            }
        }

        /// <summary>
        /// Método que usa o modelo "padrão" para retornar valores
        /// </summary>
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

        public static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("pt-br");

            Console.WriteLine($"========== Gerando {MAX_ITEM_COUNT:n} itens ==========");

            Time.Measure(WithoutIterator, SHOW_GENERATED_ITEMS);
            Time.Measure(WithIterator, SHOW_GENERATED_ITEMS);
        }
    }
}
