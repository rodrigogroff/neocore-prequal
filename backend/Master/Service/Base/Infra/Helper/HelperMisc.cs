using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Master.Service.Base.Infra.Helper
{
    public class HelperMisc
    {
        public string CapitalizeName(string name)
        {
            string[] words = name.ToLower().Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (!string.IsNullOrEmpty(words[i]))
                {
                    char[] letters = words[i].ToCharArray();
                    letters[0] = char.ToUpper(letters[0]);
                    words[i] = new string(letters);
                }
            }
            return string.Join(" ", words);
        }

        public string ToLower(string x)
        {
            if (x == null)
                return "";
            else
                return x.ToLower();
        }

        public string ToLower(long? x)
        {
            if (x == null)
                return "";
            else
                return x.ToString().ToLower();
        }

        public List<long> GetVector(string input)
        {
            List<long> lstBas = new List<long>();
            if (!string.IsNullOrEmpty(input))
                lstBas = input.TrimEnd(',').Split(',').Select(y => Convert.ToInt64(y)).ToList();
            return lstBas;
        }

        public string GenerateSixDigitNumber()
        {
            Random random = new Random();
            return random.Next(100000, 1000000).ToString();
        }

        public long ComputeEmailIndex(string email)
        {
            var firstName = email.Trim().ToLower().Split('@')[0];
            var str = Convert.ToInt32(firstName[0]).ToString();

            if (firstName.Length >= 2)
            {
                str += Convert.ToInt32(firstName[1]).ToString();
            }
            else
            {
                str += "0"; // Add a placeholder if firstName has less than 2 characters
            }

            if (firstName.Length >= 3)
            {
                str += Convert.ToInt32(firstName[firstName.Length - 1]).ToString();
            }
            else
            {
                str += "00";
            }

            return Convert.ToInt64(str);
        }

        public long GetCents(string s)
        {
            return Convert.ToInt64(s.Trim().Replace("R$", "").Replace(".", "").Replace(",", ""));
        }

        public string ExtractNumber(string s)
        {
            var ret = new StringBuilder();

            if (s!=null)
                foreach (var x in s)    
                    if (Char.IsNumber(x))
                        ret.Append(x);
            
            return ret.ToString();
        }

        public string GetMoney(long number)
        {
            double decimalNumber = (double)number / 100;
            return decimalNumber.ToString("#,0.00", System.Globalization.CultureInfo.GetCultureInfo("pt-BR"));
        }

        public DateTime D(string s)
        {
            if (string.IsNullOrEmpty(s))
                return new DateTime(1, 1, 1);

            try
            {
                return new DateTime(Convert.ToInt32(s.Substring(6, 4)), Convert.ToInt32(s.Substring(3, 2)), Convert.ToInt32(s[..2]), 0, 0, 0, DateTimeKind.Unspecified);
            }
            catch (Exception ex)
            {
                return new DateTime(1, 1, 1);
            }
        }

        public DateTime D(long year, long month, long day)
        {
            return new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day), 0, 0, 0, DateTimeKind.Unspecified);
        }

        public DateTime? ParseDate(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return null;

            return DateTime.TryParseExact(
                raw,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var result
            ) ? result : null;
        }

        public void ExtractDate(string s, ref long year, ref long month, ref long day)
        {
            day = Convert.ToInt64(s[..2]);
            month = Convert.ToInt64(s.Substring(3, 2));
            year = Convert.ToInt64(s.Substring(6, 4));
        }

        public string GetTimePassed(long year, long month, long day)
        {
            DateTime givenDate = new DateTime((int)year, (int)month, (int)day);
            DateTime today = DateTime.Today;

            int years = today.Year - givenDate.Year;
            int months = today.Month - givenDate.Month;

            if (months < 0)
            {
                years--;
                months += 12;
            }

            string yearString = years > 1 ? "anos" : "ano";
            string monthString = months > 1 ? "meses" : "mês";

            if (years > 0)
                return $"{years} {yearString}, {months} {monthString}";
            else
                return $"{months} {monthString}";
        }

        public string ApplyFormat(string unmaskedString, string format)
        {
            if (string.IsNullOrEmpty(unmaskedString))
                return "";

            StringBuilder formattedString = new StringBuilder();
            int index = 0;

            foreach (char c in format)
                if (c == '0')
                {
                    if (index < unmaskedString.Length)
                    {
                        formattedString.Append(unmaskedString[index]);
                        index++;
                    }
                }
                else
                    formattedString.Append(c);

            return formattedString.ToString();
        }
    }
}
