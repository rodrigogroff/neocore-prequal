using System;
using System.Globalization;

namespace Master.Service.Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly CultureInfo BrazilCulture = new CultureInfo("pt-BR");

        /// <summary>
        /// Converte uma string de data no formato brasileiro (dd/MM/yyyy) para DateTime
        /// </summary>
        /// <param name="dateString">String no formato dd/MM/yyyy (ex: "19/08/2004")</param>
        /// <returns>DateTime ou null se a conversão falhar</returns>
        public static DateTime? ToDateTimeBr(this string dateString)
        {
            if (string.IsNullOrWhiteSpace(dateString))
                return null;

            // Tenta converter usando o formato brasileiro padrão
            if (DateTime.TryParseExact(
                dateString,
                "dd/MM/yyyy",
                BrazilCulture,
                DateTimeStyles.None,
                out DateTime result))
            {
                return result;
            }

            // Tenta conversão genérica como fallback
            if (DateTime.TryParse(dateString, BrazilCulture, DateTimeStyles.None, out result))
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// Converte uma string de data no formato brasileiro para DateTime (lança exceção se falhar)
        /// </summary>
        /// <param name="dateString">String no formato dd/MM/yyyy</param>
        /// <returns>DateTime</returns>
        /// <exception cref="FormatException">Se a string não puder ser convertida</exception>
        public static DateTime ToDateTimeBrStrict(this string dateString)
        {
            if (string.IsNullOrWhiteSpace(dateString))
                throw new ArgumentException("Data não pode ser nula ou vazia", nameof(dateString));

            return DateTime.ParseExact(dateString, "dd/MM/yyyy", BrazilCulture, DateTimeStyles.None);
        }

        /// <summary>
        /// Formata um DateTime para o formato brasileiro (dd/MM/yyyy)
        /// </summary>
        /// <param name="date">DateTime para formatar</param>
        /// <returns>String no formato dd/MM/yyyy</returns>
        public static string ToStringBr(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy", BrazilCulture);
        }

        /// <summary>
        /// Formata um DateTime nullable para o formato brasileiro
        /// </summary>
        /// <param name="date">DateTime nullable</param>
        /// <returns>String no formato dd/MM/yyyy ou string vazia se null</returns>
        public static string ToStringBr(this DateTime? date)
        {
            return date?.ToString("dd/MM/yyyy", BrazilCulture) ?? string.Empty;
        }
    }
}