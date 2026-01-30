using Dapper;
using Master.Entity.Database.Domain.Company;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Master.Repository.Domain.Company
{
    public interface ICompanyRepository
    {
        Tb_Company GetCompany(long id);
        Tb_Company GetCompany(Guid client_id);
        List<Tb_Company> GetCompanies();
        long InsertCompany(Tb_Company mdl);
        void UpdateCompany(Tb_Company mdl);

        Tb_CompanyFinanceiro GetCompanyFinanceiro(int fkCompany);
        long InsertCompanyFinanceiro(Tb_CompanyFinanceiro mdl);
        void UpdateCompanyFinanceiro(Tb_CompanyFinanceiro mdl);

        Tb_CompanyFatura GetCompanyFatura(int fkCompany, int year, int month);
        long InsertCompanyFatura(Tb_CompanyFatura mdl);
        void UpdateCompanyFatura(Tb_CompanyFatura mdl);
    }

    public class CompanyRepository : BaseRepository, ICompanyRepository
    {
        // ==================== COMPANY ====================

        public Tb_Company GetCompany(long id)
        {
            const string query = "select * from \"Company\" where \"id\"=@id";
            return db.QueryFirstOrDefault<Tb_Company>(query, new { id });
        }

        public Tb_Company GetCompany(Guid client_id)
        {
            const string query = "select * from \"Company\" where \"client_id\"=@client_id";
            return db.QueryFirstOrDefault<Tb_Company>(query, new { client_id });
        }

        public List<Tb_Company> GetCompanies()
        {
            const string query = "select * from \"Company\" order by \"id\" desc";
            return db.Query<Tb_Company>(query).ToList();
        }

        public long InsertCompany(Tb_Company mdl)
        {
            const string query =
                "INSERT INTO \"Company\" (\"stName\",\"client_id\",\"stSecret\",\"bActive\") " +
                "VALUES (@stName,@client_id,@stSecret,@bActive)" +
                "RETURNING \"id\";";
            
            return db.ExecuteScalar<long>(query, mdl);
        }

        public void UpdateCompany(Tb_Company mdl)
        {
            const string query =
                "UPDATE \"Company\" SET " +
                "\"stName\"=@stName," +
                "\"client_id\"=@client_id," +
                "\"stSecret\"=@stSecret," +
                "\"bActive\"=@bActive " +
                "WHERE \"id\"=@id";

            db.Execute(query, mdl);
        }

        // ==================== COMPANY FINANCEIRO ====================

        public Tb_CompanyFinanceiro GetCompanyFinanceiro(int fkCompany)
        {
            const string query = "select * from \"CompanyFinanceiro\" where \"fkCompany\"=@fkCompany";
            return db.QueryFirstOrDefault<Tb_CompanyFinanceiro>(query, new { fkCompany });
        }

        public long InsertCompanyFinanceiro(Tb_CompanyFinanceiro mdl)
        {
            const string query =
                "INSERT INTO \"CompanyFinanceiro\" (" +
                "\"fkCompany\"," +
                "\"bActiveSubL1\"," +
                "\"bActiveSubL2\"," +
                "\"vrSubscriptionL1\"," +
                "\"vrL1Transaction\"," +
                "\"vrL1TransactionItem\"," +
                "\"vrSubscriptionL2\"," +
                "\"vrL2Transaction\"," +
                "\"vrL2TransactionItem\"" +
                ") VALUES (" +
                "@fkCompany," +
                "@bActiveSubL1," +
                "@bActiveSubL2," +
                "@vrSubscriptionL1," +
                "@vrL1Transaction," +
                "@vrL1TransactionItem," +
                "@vrSubscriptionL2," +
                "@vrL2Transaction," +
                "@vrL2TransactionItem" +
                ") RETURNING \"id\";";
            
            return db.ExecuteScalar<long>(query, mdl);
        }

        public void UpdateCompanyFinanceiro(Tb_CompanyFinanceiro mdl)
        {
            const string query =
                "UPDATE \"CompanyFinanceiro\" SET " +
                "\"bActiveSubL1\"=@bActiveSubL1," +
                "\"bActiveSubL2\"=@bActiveSubL2," +
                "\"vrSubscriptionL1\"=@vrSubscriptionL1," +
                "\"vrL1Transaction\"=@vrL1Transaction," +
                "\"vrL1TransactionItem\"=@vrL1TransactionItem," +
                "\"vrSubscriptionL2\"=@vrSubscriptionL2," +
                "\"vrL2Transaction\"=@vrL2Transaction," +
                "\"vrL2TransactionItem\"=@vrL2TransactionItem " +
                "WHERE \"id\"=@id";

            db.Execute(query, mdl);
        }

        // ==================== COMPANY FATURA ====================

        public Tb_CompanyFatura GetCompanyFatura(int fkCompany, int year, int month)
        {
            const string query =
                "SELECT * FROM \"CompanyFatura\" " +
                "WHERE \"fkCompany\"=@fkCompany AND \"nuYear\"=@year AND \"nuMonth\"=@month";

            return db.QueryFirstOrDefault<Tb_CompanyFatura>(query, new { fkCompany, year, month });
        }

        public long InsertCompanyFatura(Tb_CompanyFatura mdl)
        {
            const string query =
                "INSERT INTO \"CompanyFatura\" (" +
                "\"fkCompany\"," +
                "\"nuYear\"," +
                "\"nuMonth\"," +
                "\"vrSubscriptionL1\"," +
                "\"vrL1Transaction\"," +
                "\"vrL1TransactionItem\"," +
                "\"nuQtdL1Trans\"," +
                "\"nuQtdL1TransItem\"," +
                "\"vrL1TransactionTotal\"," +
                "\"vrL1TransactionItemTotal\"," +
                "\"vrSubscriptionL2\"," +
                "\"vrL2Transaction\"," +
                "\"vrL2TransactionItem\"," +
                "\"nuQtdL2Trans\"," +
                "\"nuQtdL2TransItem\"," +
                "\"vrL2TransactionTotal\"," +
                "\"vrL2TransactionItemTotal\"," +
                "\"vrImpostos\"," +
                "\"vrSubTotal\"," +
                "\"vrTotal\"" +
                ") VALUES (" +
                "@fkCompany," +
                "@nuYear," +
                "@nuMonth," +
                "@vrSubscriptionL1," +
                "@vrL1Transaction," +
                "@vrL1TransactionItem," +
                "@nuQtdL1Trans," +
                "@nuQtdL1TransItem," +
                "@vrL1TransactionTotal," +
                "@vrL1TransactionItemTotal," +
                "@vrSubscriptionL2," +
                "@vrL2Transaction," +
                "@vrL2TransactionItem," +
                "@nuQtdL2Trans," +
                "@nuQtdL2TransItem," +
                "@vrL2TransactionTotal," +
                "@vrL2TransactionItemTotal," +
                "@vrImpostos," +
                "@vrSubTotal," +
                "@vrTotal" +
                ") RETURNING \"id\";";

            return db.ExecuteScalar<long>(query, mdl);
        }

        public void UpdateCompanyFatura(Tb_CompanyFatura mdl)
        {
            const string query =
                "UPDATE \"CompanyFatura\" SET " +
                "\"nuYear\"=@nuYear," +
                "\"nuMonth\"=@nuMonth," +
                "\"vrSubscriptionL1\"=@vrSubscriptionL1," +
                "\"vrL1Transaction\"=@vrL1Transaction," +
                "\"vrL1TransactionItem\"=@vrL1TransactionItem," +
                "\"nuQtdL1Trans\"=@nuQtdL1Trans," +
                "\"nuQtdL1TransItem\"=@nuQtdL1TransItem," +
                "\"vrL1TransactionTotal\"=@vrL1TransactionTotal," +
                "\"vrL1TransactionItemTotal\"=@vrL1TransactionItemTotal," +
                "\"vrSubscriptionL2\"=@vrSubscriptionL2," +
                "\"vrL2Transaction\"=@vrL2Transaction," +
                "\"vrL2TransactionItem\"=@vrL2TransactionItem," +
                "\"nuQtdL2Trans\"=@nuQtdL2Trans," +
                "\"nuQtdL2TransItem\"=@nuQtdL2TransItem," +
                "\"vrL2TransactionTotal\"=@vrL2TransactionTotal," +
                "\"vrL2TransactionItemTotal\"=@vrL2TransactionItemTotal," +
                "\"vrImpostos\"=@vrImpostos," +
                "\"vrTotal\"=@vrTotal " +
                "WHERE \"id\"=@id";

            db.Execute(query, mdl);
        }
    }
}
