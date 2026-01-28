using Dapper;
using Master.Entity.Database.Domain.Prequal;
using Npgsql;
using NpgsqlTypes;
using System.Collections.Generic;
using System.Linq;

namespace Master.Repository.Domain.Prequal
{
    public interface IPrequalRepository
    {
        Tb_PrequalLeilaoConfig? GetPrequalLeilaoConfig(int fkCompany);                
        long InsertPrequalLeilaoConfig(Tb_PrequalLeilaoConfig mdl, bool retId = false);
        void UpdatePrequalLeilaoConfig(Tb_PrequalLeilaoConfig mdl);

        long InsertLogProcPrequalLeilao(Tb_LogProcPrequalLeilao mdl, bool retId = false);
        List<Tb_LogProcPrequalLeilao> GetLogs(int fkCompany, int year, int month);
    }

    public class PrequalRepository : BaseRepository, IPrequalRepository
    {
        public Tb_PrequalLeilaoConfig? GetPrequalLeilaoConfig(int fkCompany)
        {
            const string query = "select * from \"PrequalLeilaoConfig\" where \"fkCompany\"=@fkCompany";

            return db.QueryFirstOrDefault<Tb_PrequalLeilaoConfig>(query, new { fkCompany });
        }

        public void SetParamsPrequalLeilaoConfig(NpgsqlCommand cmd, Tb_PrequalLeilaoConfig mdl)
        {
            const
               string
                   id = "id",
                   fkCompany = "fkCompany",
                   bEmpregadorCnpj = "bEmpregadorCnpj",
                   bEmpregadorCpf = "bEmpregadorCpf",
                   bPep = "bPep",
                   bAlertaSaude = "bAlertaSaude",
                   bAlertaAvisoPrevio = "bAlertaAvisoPrevio",
                   vrLibMin = "vrLibMin",
                   vrLibMax = "vrLibMax",
                   nuParcMin = "nuParcMin",
                   nuParcMax = "nuParcMax",
                   nuIdadeMin = "nuIdadeMin",
                   nuIdadeMax = "nuIdadeMax",
                   vrMargemMin = "vrMargemMin",
                   vrMargemMax = "vrMargemMax",
                   nuMesesAdmissaoMin = "nuMesesAdmissaoMin",
                   nuMesesAdmissaoMax = "nuMesesAdmissaoMax",
                   nuMesesAberturaEmpresaMin = "nuMesesAberturaEmpresaMin";

            cmd.Parameters.AddRange(new NpgsqlParameter[]
            {
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = id, Value = mdl.id },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = fkCompany, Value = GetNull(mdl.fkCompany) },
                new() { NpgsqlDbType = NpgsqlDbType.Boolean, ParameterName = bEmpregadorCnpj, Value = GetNull(mdl.bEmpregadorCnpj) },
                new() { NpgsqlDbType = NpgsqlDbType.Boolean, ParameterName = bEmpregadorCpf, Value = GetNull(mdl.bEmpregadorCpf) },
                new() { NpgsqlDbType = NpgsqlDbType.Boolean, ParameterName = bPep, Value = GetNull(mdl.bPep) },
                new() { NpgsqlDbType = NpgsqlDbType.Boolean, ParameterName = bAlertaSaude, Value = GetNull(mdl.bAlertaSaude) },
                new() { NpgsqlDbType = NpgsqlDbType.Boolean, ParameterName = bAlertaAvisoPrevio, Value = GetNull(mdl.bAlertaAvisoPrevio) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = vrLibMin, Value = mdl.vrLibMin },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = vrLibMax, Value = mdl.vrLibMax },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuParcMin, Value = mdl.nuParcMin },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuParcMax, Value = mdl.nuParcMax },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuIdadeMin, Value = mdl.nuIdadeMin },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuIdadeMax, Value = mdl.nuIdadeMax },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = vrMargemMin, Value = mdl.vrMargemMin },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = vrMargemMax, Value = mdl.vrMargemMax },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuMesesAdmissaoMin, Value = mdl.nuMesesAdmissaoMin },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuMesesAdmissaoMax, Value = mdl.nuMesesAdmissaoMax },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuMesesAberturaEmpresaMin, Value = mdl.nuMesesAberturaEmpresaMin },
            });
        }

        public long InsertPrequalLeilaoConfig(Tb_PrequalLeilaoConfig mdl, bool retId = false)
        {
            const string query =
                "INSERT INTO \"PrequalLeilaoConfig\" (\"fkCompany\",\"bEmpregadorCnpj\",\"bEmpregadorCpf\",\"bPep\",\"bAlertaSaude\",\"bAlertaAvisoPrevio\",\"vrLibMin\"," +
                "\"vrLibMax\",\"nuParcMin\",\"nuParcMax\",\"nuIdadeMin\",\"nuIdadeMax\",\"vrMargemMin\",\"vrMargemMax\",\"nuMesesAdmissaoMin\",\"nuMesesAdmissaoMax\",\"nuMesesAberturaEmpresaMin\"" +
                ") VALUES " +
                "(@fkCompany,@bEmpregadorCnpj,@bEmpregadorCpf,@bPep,@bAlertaSaude,@bAlertaAvisoPrevio,@vrLibMin,@vrLibMax,@nuParcMin,@nuParcMax,@nuIdadeMin,@nuIdadeMax,@vrMargemMin,@vrMargemMax," +
                "@nuMesesAdmissaoMin,@nuMesesAdmissaoMax,@nuMesesAberturaEmpresaMin);";

            const string currval = "select currval('public.\"PrequalLeilaoConfig_id_seq\"');";

            using var cmd = new NpgsqlCommand(retId ? query + currval : query, db);
            SetParamsPrequalLeilaoConfig(cmd, mdl);
            if (retId) return (long)cmd.ExecuteScalar();
            cmd.ExecuteNonQuery();
            return 0;
        }

        public void UpdatePrequalLeilaoConfig(Tb_PrequalLeilaoConfig mdl)
        {
            const string query = "update \"PrequalLeilaoConfig\" set " +
                "\"bEmpregadorCnpj\"=@bEmpregadorCnpj," +
                "\"bEmpregadorCpf\"=@bEmpregadorCpf," +
                "\"bPep\"=@bPep," +
                "\"bAlertaSaude\"=@bAlertaSaude," +
                "\"bAlertaAvisoPrevio\"=@bAlertaAvisoPrevio," +
                "\"vrLibMin\"=@vrLibMin," +
                "\"vrLibMax\"=@vrLibMax," +
                "\"nuParcMin\"=@nuParcMin," +
                "\"nuParcMax\"=@nuParcMax, " +
                "\"nuIdadeMin\"=@nuIdadeMin," +
                "\"nuIdadeMax\"=@nuIdadeMax," +
                "\"vrMargemMin\"=@vrMargemMin," +
                "\"vrMargemMax\"=@vrMargemMax," +
                "\"nuMesesAdmissaoMin\"=@nuMesesAdmissaoMin," +
                "\"nuMesesAdmissaoMax\"=@nuMesesAdmissaoMax, " +
                "\"nuMesesAberturaEmpresaMin\"=@nuMesesAberturaEmpresaMin " +
                " where id=@id";

            using var cmd = new NpgsqlCommand(query, db);
            SetParamsPrequalLeilaoConfig(cmd, mdl);
            cmd.ExecuteNonQuery();
        }

       

        public void SetParamsLogProcPrequalLeilao(NpgsqlCommand cmd, Tb_LogProcPrequalLeilao mdl)
        {
            const
               string
                    id = "id",
                    fkCompany = "fkCompany",
                    dtLog = "dtLog",
                    nuYear = "nuYear",
                    nuMonth = "nuMonth",
                    nuDay = "nuDay",
                    nuMinute = "nuMinute",
                    nuHour = "nuHour",
                    nuTotMS = "nuTotMS",
                    nuTotProcs = "nuTotProcs",
                    nuTotQualificadas = "nuTotQualificadas",
                    nuTotRejeitadas = "nuTotRejeitadas",
                    nuFilter1 = "nuFilter1",
                    nuFilter2 = "nuFilter2",
                    nuFilter3 = "nuFilter3",
                    nuFilter4 = "nuFilter4",
                    nuFilter5 = "nuFilter5",
                    nuFilter6 = "nuFilter6",
                    nuFilter7 = "nuFilter7",
                    nuFilter8 = "nuFilter8",
                    nuFilter9 = "nuFilter9",
                    nuFilter10 = "nuFilter10",
                    nuFilter11 = "nuFilter11",
                    nuFilter12 = "nuFilter12",
                    nuFilter13 = "nuFilter13",
                    nuFilter14 = "nuFilter14",
                    nuFilter15 = "nuFilter15",
                    nuFilter16 = "nuFilter16",
                    nuFilter17 = "nuFilter17",
                    nuPctFilter = "nuPctFilter";

            cmd.Parameters.AddRange(new NpgsqlParameter[]
            {
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = id, Value = mdl.id },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = fkCompany, Value = GetNull(mdl.fkCompany) },
                new() { NpgsqlDbType = NpgsqlDbType.Date, ParameterName = dtLog, Value = GetNull(mdl.dtLog) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuYear, Value = GetNull(mdl.nuYear) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuMonth, Value = GetNull(mdl.nuMonth) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuDay, Value = GetNull(mdl.nuDay) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuMinute, Value = GetNull(mdl.nuMinute) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuHour, Value = GetNull(mdl.nuHour) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuTotMS, Value = GetNull(mdl.nuTotMS) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuTotProcs, Value = GetNull(mdl.nuTotProcs) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuTotQualificadas, Value = GetNull(mdl.nuTotQualificadas) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuTotRejeitadas, Value = GetNull(mdl.nuTotRejeitadas) },
                new() { NpgsqlDbType = NpgsqlDbType.Numeric, ParameterName = nuPctFilter, Value = GetNull(mdl.nuPctFilter) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuFilter1, Value = GetNull(mdl.nuFilter1) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuFilter2, Value = GetNull(mdl.nuFilter2) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuFilter3, Value = GetNull(mdl.nuFilter3) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuFilter4, Value = GetNull(mdl.nuFilter4) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuFilter5, Value = GetNull(mdl.nuFilter5) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuFilter6, Value = GetNull(mdl.nuFilter6) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuFilter7, Value = GetNull(mdl.nuFilter7) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuFilter8, Value = GetNull(mdl.nuFilter8) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuFilter9, Value = GetNull(mdl.nuFilter9) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuFilter10, Value = GetNull(mdl.nuFilter10) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuFilter11, Value = GetNull(mdl.nuFilter11) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuFilter12, Value = GetNull(mdl.nuFilter12) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuFilter13, Value = GetNull(mdl.nuFilter13) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuFilter14, Value = GetNull(mdl.nuFilter14) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuFilter15, Value = GetNull(mdl.nuFilter15) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuFilter16, Value = GetNull(mdl.nuFilter16) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuFilter17, Value = GetNull(mdl.nuFilter17) },
            });
        }

        public long InsertLogProcPrequalLeilao(Tb_LogProcPrequalLeilao mdl, bool retId = false)
        {
            const string query =
                "INSERT INTO \"LogProcPrequalLeilao\" (\"fkCompany\",\"dtLog\",\"nuYear\",\"nuMonth\",\"nuDay\",\"nuHour\",\"nuMinute\"," +
                "\"nuTotMS\",\"nuTotProcs\",\"nuTotQualificadas\",\"nuTotRejeitadas\",\"nuPctFilter\"" +
                ",\"nuFilter1\",\"nuFilter2\",\"nuFilter3\",\"nuFilter4\",\"nuFilter5\",\"nuFilter6\",\"nuFilter7\",\"nuFilter8\",\"nuFilter9\",\"nuFilter10\"" +
                ",\"nuFilter11\",\"nuFilter12\",\"nuFilter13\",\"nuFilter14\",\"nuFilter15\",\"nuFilter16\",\"nuFilter17\"" +
                ") VALUES " +
                "(@fkCompany,@dtLog,@nuYear,@nuMonth,@nuDay,@nuHour,@nuMinute,@nuTotMS,@nuTotProcs,@nuTotQualificadas,@nuTotRejeitadas,@nuPctFilter," +
                "@nuFilter1,@nuFilter2,@nuFilter3,@nuFilter4,@nuFilter5,@nuFilter6,@nuFilter7,@nuFilter8,@nuFilter9,@nuFilter10," +
                "@nuFilter11,@nuFilter12,@nuFilter13,@nuFilter14,@nuFilter15,@nuFilter16,@nuFilter17" + 
                ");";

            const string currval = "select currval('public.\"LogProcPrequalLeilao_id_seq\"');";

            using var cmd = new NpgsqlCommand(retId ? query + currval : query, db);
            SetParamsLogProcPrequalLeilao(cmd, mdl);
            if (retId) return (long)cmd.ExecuteScalar();
            cmd.ExecuteNonQuery();
            return 0;
        }

        public List<Tb_LogProcPrequalLeilao> GetLogs(int fkCompany, int year, int month)
        {
            const string query = "select * from \"LogProcPrequalLeilao\" where \"fkCompany\"=@fkCompany and \"nuYear\"=@year and \"nuMonth\"=@month order by \"dtLog\"";

            return db.Query<Tb_LogProcPrequalLeilao>(query, new { fkCompany, year, month } ).ToList();
        }
    }
}
