using Dapper;
using Master.Entity.Database.Domain.Company;
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
                   bSimples = "bSimples",
                   bMei = "bMei",
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
                   nuMesesAdmissaoMax = "nuMesesAdmissaoMax";

            cmd.Parameters.AddRange(new NpgsqlParameter[]
            {
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = id, Value = mdl.id },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = fkCompany, Value = GetNull(mdl.fkCompany) },
                new() { NpgsqlDbType = NpgsqlDbType.Boolean, ParameterName = bEmpregadorCnpj, Value = GetNull(mdl.bEmpregadorCnpj) },
                new() { NpgsqlDbType = NpgsqlDbType.Boolean, ParameterName = bEmpregadorCpf, Value = GetNull(mdl.bEmpregadorCpf) },
                new() { NpgsqlDbType = NpgsqlDbType.Boolean, ParameterName = bPep, Value = GetNull(mdl.bPep) },
                new() { NpgsqlDbType = NpgsqlDbType.Boolean, ParameterName = bAlertaSaude, Value = GetNull(mdl.bAlertaSaude) },
                new() { NpgsqlDbType = NpgsqlDbType.Boolean, ParameterName = bAlertaAvisoPrevio, Value = GetNull(mdl.bAlertaAvisoPrevio) },
                new() { NpgsqlDbType = NpgsqlDbType.Boolean, ParameterName = bSimples, Value = GetNull(mdl.bSimples) },
                new() { NpgsqlDbType = NpgsqlDbType.Boolean, ParameterName = bMei, Value = GetNull(mdl.bMei) },
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
            });
        }

        public long InsertPrequalLeilaoConfig(Tb_PrequalLeilaoConfig mdl, bool retId = false)
        {
            const string query =
                "INSERT INTO \"PrequalLeilaoConfig\" (\"fkCompany\",\"bEmpregadorCnpj\",\"bEmpregadorCpf\",\"bPep\",\"bAlertaSaude\",\"bAlertaAvisoPrevio\",\"bSimples\",\"bMei\",\"vrLibMin\"," +
                "\"vrLibMax\",\"nuParcMin\",\"nuParcMax\",\"nuIdadeMin\",\"nuIdadeMax\",\"vrMargemMin\",\"vrMargemMax\",\"nuMesesAdmissaoMin\",\"nuMesesAdmissaoMax\"" +
                ") VALUES " +
                "(@fkCompany,@bEmpregadorCnpj,@bEmpregadorCpf,@bPep,@bAlertaSaude,@bAlertaAvisoPrevio,@bSimples,@bMei,@vrLibMin,@vrLibMax,@nuParcMin,@nuParcMax,@nuIdadeMin,@nuIdadeMax,@vrMargemMin,@vrMargemMax," +
                "@nuMesesAdmissaoMin,@nuMesesAdmissaoMax);";

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
                "\"bSimples\"=@bSimples," +
                "\"bMei\"=@bMei," +
                "\"vrLibMin\"=@vrLibMin," +
                "\"vrLibMax\"=@vrLibMax," +
                "\"nuParcMin\"=@nuParcMin," +
                "\"nuParcMax\"=@nuParcMax, " +
                "\"nuIdadeMin\"=@nuIdadeMin," +
                "\"nuIdadeMax\"=@nuIdadeMax," +
                "\"vrMargemMin\"=@vrMargemMin," +
                "\"vrMargemMax\"=@vrMargemMax," +
                "\"nuMesesAdmissaoMin\"=@nuMesesAdmissaoMin," +
                "\"nuMesesAdmissaoMax\"=@nuMesesAdmissaoMax " +
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

            });
        }

        public long InsertLogProcPrequalLeilao(Tb_LogProcPrequalLeilao mdl, bool retId = false)
        {
            const string query =
                "INSERT INTO \"LogProcPrequalLeilao\" (\"fkCompany\",\"dtLog\",\"nuYear\",\"nuMonth\",\"nuDay\",\"nuHour\",\"nuMinute\"," +
                "\"nuTotMS\",\"nuTotProcs\",\"nuTotQualificadas\",\"nuTotRejeitadas\",\"nuPctFilter\"" +
                ") VALUES " +
                "(@fkCompany,@dtLog,@nuYear,@nuMonth,@nuDay,@nuHour,@nuMinute,@nuTotMS,@nuTotProcs,@nuTotQualificadas,@nuTotRejeitadas,@nuPctFilter);";

            const string currval = "select currval('public.\"LogProcPrequalLeilao_id_seq\"');";

            using var cmd = new NpgsqlCommand(retId ? query + currval : query, db);
            SetParamsLogProcPrequalLeilao(cmd, mdl);
            if (retId) return (long)cmd.ExecuteScalar();
            cmd.ExecuteNonQuery();
            return 0;
        }

        public List<Tb_LogProcPrequalLeilao> GetLogs(int fkCompany, int year, int month)
        {
            const string query = "select * from \"LogProcPrequalLeilao\" where \"fkCompany\"=@fkCompany and \"nuYear\"=@year and \"nuMonth\"=@month";

            return db.Query<Tb_LogProcPrequalLeilao>(query, new { fkCompany, year, month } ).ToList();
        }
    }
}
