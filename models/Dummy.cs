using System.Globalization;

namespace Program.Models
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
}