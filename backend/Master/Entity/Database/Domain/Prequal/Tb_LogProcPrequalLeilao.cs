using System;

namespace Master.Entity.Database.Domain.Prequal
{
    public class Tb_LogProcPrequalLeilao
    {
        public long id { get; set; }
        public long? fkCompany { get; set; }
        public DateTime? dtLog { get; set; }
        public int? nuYear { get; set; }
        public int? nuMonth { get; set; }
        public int? nuDay { get; set; }
        public int? nuHour { get; set; }
        public int? nuMinute { get; set; }
        public int? nuTotMS { get; set; }
        public int? nuTotProcs { get; set; }
        public int? nuTotQualificadas { get; set; }
        public int? nuTotRejeitadas { get; set; }
        public double? nuPctFilter { get; set; }
    }
}
