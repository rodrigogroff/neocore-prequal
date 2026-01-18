namespace Master.Data.Const
{
    public static class TypeMonthDescription
    {
        static readonly string[] options =
            {
                "Janeiro",
                "Fevereiro",
                "Março",
                "Abril",
                "Maio",
                "Junho",
                "Julho",
                "Agosto",
                "Setembro",
                "Outubro",
                "Novembro",
                "Dezembro"
            };

        public static string ToString(long index)
        {
            if (index >= options.Length || index < 0)
                return "";

            return options[index];
        }
    }
}
