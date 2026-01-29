using Dapper;
using Master.Entity.Database.Domain.Prequal;
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
        // ==================== PREQUAL LEILAO CONFIG ====================

        public Tb_PrequalLeilaoConfig? GetPrequalLeilaoConfig(int fkCompany)
        {
            const string query = "SELECT * FROM \"PrequalLeilaoConfig\" WHERE \"fkCompany\"=@fkCompany";
            return db.QueryFirstOrDefault<Tb_PrequalLeilaoConfig>(query, new { fkCompany });
        }

        public long InsertPrequalLeilaoConfig(Tb_PrequalLeilaoConfig mdl, bool retId = false)
        {
            const string query =
                "INSERT INTO \"PrequalLeilaoConfig\" (" +
                "\"fkCompany\"," +
                "\"bEmpregadorCnpj\"," +
                "\"bEmpregadorCpf\"," +
                "\"bPep\"," +
                "\"bAlertaSaude\"," +
                "\"bAlertaAvisoPrevio\"," +
                "\"vrLibMin\"," +
                "\"vrLibMax\"," +
                "\"nuParcMin\"," +
                "\"nuParcMax\"," +
                "\"nuIdadeMin\"," +
                "\"nuIdadeMax\"," +
                "\"vrMargemMin\"," +
                "\"vrMargemMax\"," +
                "\"nuMesesAdmissaoMin\"," +
                "\"nuMesesAdmissaoMax\"," +
                "\"nuMesesAberturaEmpresaMin\"" +
                ") VALUES (" +
                "@fkCompany," +
                "@bEmpregadorCnpj," +
                "@bEmpregadorCpf," +
                "@bPep," +
                "@bAlertaSaude," +
                "@bAlertaAvisoPrevio," +
                "@vrLibMin," +
                "@vrLibMax," +
                "@nuParcMin," +
                "@nuParcMax," +
                "@nuIdadeMin," +
                "@nuIdadeMax," +
                "@vrMargemMin," +
                "@vrMargemMax," +
                "@nuMesesAdmissaoMin," +
                "@nuMesesAdmissaoMax," +
                "@nuMesesAberturaEmpresaMin" +
                ") RETURNING \"id\";";

            if (retId)
            {
                return db.ExecuteScalar<long>(query, mdl);
            }

            db.Execute(query, mdl);
            return 0;
        }

        public void UpdatePrequalLeilaoConfig(Tb_PrequalLeilaoConfig mdl)
        {
            const string query =
                "UPDATE \"PrequalLeilaoConfig\" SET " +
                "\"bEmpregadorCnpj\"=@bEmpregadorCnpj," +
                "\"bEmpregadorCpf\"=@bEmpregadorCpf," +
                "\"bPep\"=@bPep," +
                "\"bAlertaSaude\"=@bAlertaSaude," +
                "\"bAlertaAvisoPrevio\"=@bAlertaAvisoPrevio," +
                "\"vrLibMin\"=@vrLibMin," +
                "\"vrLibMax\"=@vrLibMax," +
                "\"nuParcMin\"=@nuParcMin," +
                "\"nuParcMax\"=@nuParcMax," +
                "\"nuIdadeMin\"=@nuIdadeMin," +
                "\"nuIdadeMax\"=@nuIdadeMax," +
                "\"vrMargemMin\"=@vrMargemMin," +
                "\"vrMargemMax\"=@vrMargemMax," +
                "\"nuMesesAdmissaoMin\"=@nuMesesAdmissaoMin," +
                "\"nuMesesAdmissaoMax\"=@nuMesesAdmissaoMax," +
                "\"nuMesesAberturaEmpresaMin\"=@nuMesesAberturaEmpresaMin " +
                "WHERE \"id\"=@id";

            db.Execute(query, mdl);
        }

        // ==================== LOG PROC PREQUAL LEILAO ====================

        public long InsertLogProcPrequalLeilao(Tb_LogProcPrequalLeilao mdl, bool retId = false)
        {
            const string query =
                "INSERT INTO \"LogProcPrequalLeilao\" (" +
                "\"fkCompany\"," +
                "\"dtLog\"," +
                "\"nuYear\"," +
                "\"nuMonth\"," +
                "\"nuDay\"," +
                "\"nuHour\"," +
                "\"nuMinute\"," +
                "\"nuTotMS\"," +
                "\"nuTotProcs\"," +
                "\"nuTotQualificadas\"," +
                "\"nuTotRejeitadas\"," +
                "\"nuPctFilter\"," +
                "\"nuFilter1\"," +
                "\"nuFilter2\"," +
                "\"nuFilter3\"," +
                "\"nuFilter4\"," +
                "\"nuFilter5\"," +
                "\"nuFilter6\"," +
                "\"nuFilter7\"," +
                "\"nuFilter8\"," +
                "\"nuFilter9\"," +
                "\"nuFilter10\"," +
                "\"nuFilter11\"," +
                "\"nuFilter12\"," +
                "\"nuFilter13\"," +
                "\"nuFilter14\"," +
                "\"nuFilter15\"," +
                "\"nuFilter16\"," +
                "\"nuFilter17\"" +
                ") VALUES (" +
                "@fkCompany," +
                "@dtLog," +
                "@nuYear," +
                "@nuMonth," +
                "@nuDay," +
                "@nuHour," +
                "@nuMinute," +
                "@nuTotMS," +
                "@nuTotProcs," +
                "@nuTotQualificadas," +
                "@nuTotRejeitadas," +
                "@nuPctFilter," +
                "@nuFilter1," +
                "@nuFilter2," +
                "@nuFilter3," +
                "@nuFilter4," +
                "@nuFilter5," +
                "@nuFilter6," +
                "@nuFilter7," +
                "@nuFilter8," +
                "@nuFilter9," +
                "@nuFilter10," +
                "@nuFilter11," +
                "@nuFilter12," +
                "@nuFilter13," +
                "@nuFilter14," +
                "@nuFilter15," +
                "@nuFilter16," +
                "@nuFilter17" +
                ") RETURNING \"id\";";

            if (retId)
            {
                return db.ExecuteScalar<long>(query, mdl);
            }

            db.Execute(query, mdl);
            return 0;
        }

        public List<Tb_LogProcPrequalLeilao> GetLogs(int fkCompany, int year, int month)
        {
            const string query =
                "SELECT * FROM \"LogProcPrequalLeilao\" " +
                "WHERE \"fkCompany\"=@fkCompany AND \"nuYear\"=@year AND \"nuMonth\"=@month " +
                "ORDER BY \"dtLog\"";

            return db.Query<Tb_LogProcPrequalLeilao>(query, new { fkCompany, year, month }).ToList();
        }
    }
}
