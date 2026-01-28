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

        public int nuFilter1 { get; set; }
        public int nuFilter2 { get; set; }
        public int nuFilter3 { get; set; }
        public int nuFilter4 { get; set; }
        public int nuFilter5 { get; set; }
        public int nuFilter6 { get; set; }
        public int nuFilter7 { get; set; }
        public int nuFilter8 { get; set; }
        public int nuFilter9 { get; set; }
        public int nuFilter10 { get; set; }
        public int nuFilter11 { get; set; }
        public int nuFilter12 { get; set; }
        public int nuFilter13 { get; set; }
        public int nuFilter14 { get; set; }
        public int nuFilter15 { get; set; }
        public int nuFilter16 { get; set; }
        public int nuFilter17 { get; set; }
    }
}
