using Dapper;
using Master.Entity.Database.Domain.Company;
using Npgsql;
using NpgsqlTypes;
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
        long InsertCompany(Tb_Company mdl, bool retId = false);
        void UpdateCompany(Tb_Company mdl);

        Tb_CompanyFinanceiro GetCompanyFinanceiro(int fkCompany);
        long InsertCompanyFinanceiro(Tb_CompanyFinanceiro mdl, bool retId = false);
        void UpdateCompanyFinanceiro(Tb_CompanyFinanceiro mdl);

        Tb_CompanyFatura GetCompanyFatura(int fkCompany, int year, int month);
        long InsertCompanyFatura(Tb_CompanyFatura mdl, bool retId = false);
        void UpdateCompanyFatura(Tb_CompanyFatura mdl);
    }
        
    public class CompanyRepository : BaseRepository, ICompanyRepository
    {
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

        public void SetParamsCompany(NpgsqlCommand cmd, Tb_Company mdl)
        {
            const
               string
                   id = "id",
                   stName = "stName",
                   client_id = "client_id",
                   stSecret = "stSecret",
                   bActive = "bActive";

            cmd.Parameters.AddRange(new NpgsqlParameter[]
            {
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = id, Value = mdl.id },
                new() { NpgsqlDbType = NpgsqlDbType.Uuid, ParameterName = client_id, Value = mdl.client_id },
                new() { NpgsqlDbType = NpgsqlDbType.Text, ParameterName = stName, Value = mdl.stName },
                new() { NpgsqlDbType = NpgsqlDbType.Text, ParameterName = stSecret, Value = mdl.stSecret },
                new() { NpgsqlDbType = NpgsqlDbType.Boolean, ParameterName = bActive, Value = mdl.bActive },
            });
        }

        public long InsertCompany(Tb_Company mdl, bool retId = false)
        {
            const string query =
                "INSERT INTO \"Company\" (\"stName\",\"client_id\",\"stSecret\",\"bActive\") VALUES " +
                "(@stName,@client_id,@stSecret,@bActive);";

            const string currval = "select currval('public.\"Company_id_seq\"');";

            using var cmd = new NpgsqlCommand(retId ? query + currval : query, db);
            SetParamsCompany(cmd, mdl);
            if (retId) return (long)cmd.ExecuteScalar();
            cmd.ExecuteNonQuery();
            return 0;
        }

        public void UpdateCompany(Tb_Company mdl)
        {
            const string query = "update \"Company\" set " +
                "\"stName\"=@stName," +
                "\"client_id\"=@client_id," +
                "\"stSecret\"=@stSecret," +
                "\"bActive\"=@bActive " +
                "where id=@id";

            using var cmd = new NpgsqlCommand(query, db);
            SetParamsCompany(cmd, mdl);
            cmd.ExecuteNonQuery();
        }

        public Tb_CompanyFinanceiro GetCompanyFinanceiro(int fkCompany)
        {
            const string query = "select * from \"CompanyFinanceiro\" where \"fkCompany\"=@fkCompany";

            return db.QueryFirstOrDefault<Tb_CompanyFinanceiro>(query, new { fkCompany });
        }

        public void SetParamsCompanyFinanceiro(NpgsqlCommand cmd, Tb_CompanyFinanceiro mdl)
        {
            const
               string
                   id = "id",
                   fkCompany = "fkCompany",
                   bActiveSubL1 = "bActiveSubL1",
                   bActiveSubL2 = "bActiveSubL2",
                   vrSubscriptionL1 = "vrSubscriptionL1",
                   vrL1Transaction = "vrL1Transaction",
                   vrL1TransactionItem = "vrL1TransactionItem",
                   vrSubscriptionL2 = "vrSubscriptionL2",
                   vrL2Transaction = "vrL2Transaction",
                   vrL2TransactionItem = "vrL2TransactionItem";

            cmd.Parameters.AddRange(new NpgsqlParameter[]
            {
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = id, Value = mdl.id },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = fkCompany, Value = mdl.fkCompany },
                new() { NpgsqlDbType = NpgsqlDbType.Numeric, ParameterName = bActiveSubL1, Value = GetNull(mdl.bActiveSubL1) },
                new() { NpgsqlDbType = NpgsqlDbType.Numeric, ParameterName = bActiveSubL2, Value = GetNull(mdl.bActiveSubL2) },
                new() { NpgsqlDbType = NpgsqlDbType.Numeric, ParameterName = vrSubscriptionL1, Value = GetNull(mdl.vrSubscriptionL1) },
                new() { NpgsqlDbType = NpgsqlDbType.Numeric, ParameterName = vrL1Transaction, Value = GetNull(mdl.vrL1Transaction) },
                new() { NpgsqlDbType = NpgsqlDbType.Numeric, ParameterName = vrL1TransactionItem, Value = GetNull(mdl.vrL1TransactionItem) },
                new() { NpgsqlDbType = NpgsqlDbType.Numeric, ParameterName = vrSubscriptionL2, Value = GetNull(mdl.vrSubscriptionL2) },
                new() { NpgsqlDbType = NpgsqlDbType.Numeric, ParameterName = vrL2Transaction, Value = GetNull(mdl.vrL2Transaction) },
                new() { NpgsqlDbType = NpgsqlDbType.Numeric, ParameterName = vrL2TransactionItem, Value = GetNull(mdl.vrL2TransactionItem) },
            });
        }

        public long InsertCompanyFinanceiro(Tb_CompanyFinanceiro mdl, bool retId = false)
        {
            const string query =
                "INSERT INTO \"CompanyFinanceiro\" (\"fkCompany\"," +
                "\"bActiveSubL1\",\"bActiveSubL2\"," +
                "\"vrSubscriptionL1\",\"vrL1Transaction\",\"vrL1TransactionItem\"," +
                "\"vrSubscriptionL2\",\"vrL2Transaction\",\"vrL2TransactionItem\") VALUES " +
                "(@fkCompany,@vrSubscriptionL1,@vrL1Transaction,@vrL1TransactionItem,@vrSubscriptionL2,@vrL2Transaction,@vrL2TransactionItem);";

            const string currval = "select currval('public.\"CompanyFinanceiro_id_seq\"');";

            using var cmd = new NpgsqlCommand(retId ? query + currval : query, db);
            SetParamsCompanyFinanceiro(cmd, mdl);
            if (retId) return (long)cmd.ExecuteScalar();
            cmd.ExecuteNonQuery();
            return 0;
        }

        public void UpdateCompanyFinanceiro(Tb_CompanyFinanceiro mdl)
        {
            const string query = "update \"CompanyFinanceiro\" set " +
                "\"bActiveSubL1\"=@bActiveSubL1," +
                "\"bActiveSubL2\"=@bActiveSubL2," +
                "\"vrSubscriptionL1\"=@vrSubscriptionL1," +
                "\"vrL1Transaction\"=@vrL1Transaction," +
                "\"vrL1TransactionItem\"=@vrL1TransactionItem," +
                "\"vrSubscriptionL2\"=@vrSubscriptionL2," +
                "\"vrL2Transaction\"=@vrL2Transaction," +
                "\"vrL2TransactionItem\"=@vrL2TransactionItem " +
                "where id=@id";

            using var cmd = new NpgsqlCommand(query, db);
            SetParamsCompanyFinanceiro(cmd, mdl);
            cmd.ExecuteNonQuery();
        }

        public Tb_CompanyFatura GetCompanyFatura(int fkCompany, int year, int month)
        {
            const string query = "select * from \"CompanyFatura\" where \"fkCompany\"=@fkCompany and \"nuYear\"=@year and \"nuMonth\"=@month";

            return db.QueryFirstOrDefault<Tb_CompanyFatura>(query, new { fkCompany, year, month });
        }
        
        public void SetParamsCompanyFatura(NpgsqlCommand cmd, Tb_CompanyFatura mdl)
        {
            const
                string
                    id = "id",
                    fkCompany = "fkCompany",
                    nuYear = "nuYear",
                    nuMonth = "nuMonth",

                    nuQtdL1Trans = "nuQtdL1Trans",
                    nuQtdL1TransItem = "nuQtdL1TransItem",
                    vrSubscriptionL1 = "vrSubscriptionL1",
                    vrL1Transaction = "vrL1Transaction",
                    vrL1TransactionItem = "vrL1TransactionItem",
                    vrL1TransactionTotal = "vrL1TransactionTotal",
                    vrL1TransactionItemTotal = "vrL1TransactionItemTotal",

                    nuQtdL2Trans = "nuQtdL2Trans",
                    nuQtdL2TransItem = "nuQtdL2TransItem",
                    vrSubscriptionL2 = "vrSubscriptionL2",
                    vrL2Transaction = "vrL2Transaction",
                    vrL2TransactionItem = "vrL2TransactionItem",
                    vrL2TransactionTotal = "vrL2TransactionTotal",
                    vrL2TransactionItemTotal = "vrL2TransactionItemTotal",

                    vrImpostos = "vrImpostos",
                    vrTotal = "vrTotal";

            cmd.Parameters.AddRange(new NpgsqlParameter[]
            {
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = id, Value = mdl.id },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = fkCompany, Value = mdl.fkCompany },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuYear, Value = GetNull(mdl.nuYear) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuMonth, Value = GetNull(mdl.nuMonth) },

                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuQtdL1Trans, Value = GetNull(mdl.nuQtdL1Trans) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuQtdL1TransItem, Value = GetNull(mdl.nuQtdL1TransItem) },
                new() { NpgsqlDbType = NpgsqlDbType.Numeric, ParameterName = vrSubscriptionL1, Value = GetNull(mdl.vrSubscriptionL1) },
                new() { NpgsqlDbType = NpgsqlDbType.Numeric, ParameterName = vrL1Transaction, Value = GetNull(mdl.vrL1Transaction) },
                new() { NpgsqlDbType = NpgsqlDbType.Numeric, ParameterName = vrL1TransactionItem, Value = GetNull(mdl.vrL1TransactionItem) },
                new() { NpgsqlDbType = NpgsqlDbType.Numeric, ParameterName = vrL1TransactionTotal, Value = GetNull(mdl.vrL1TransactionTotal) },
                new() { NpgsqlDbType = NpgsqlDbType.Numeric, ParameterName = vrL1TransactionItemTotal, Value = GetNull(mdl.vrL1TransactionItemTotal) },

                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuQtdL2Trans, Value = GetNull(mdl.nuQtdL2Trans) },
                new() { NpgsqlDbType = NpgsqlDbType.Integer, ParameterName = nuQtdL2TransItem, Value = GetNull(mdl.nuQtdL2TransItem) },
                new() { NpgsqlDbType = NpgsqlDbType.Numeric, ParameterName = vrSubscriptionL2, Value = GetNull(mdl.vrSubscriptionL2) },
                new() { NpgsqlDbType = NpgsqlDbType.Numeric, ParameterName = vrL2Transaction, Value = GetNull(mdl.vrL2Transaction) },
                new() { NpgsqlDbType = NpgsqlDbType.Numeric, ParameterName = vrL2TransactionItem, Value = GetNull(mdl.vrL2TransactionItem) },
                new() { NpgsqlDbType = NpgsqlDbType.Numeric, ParameterName = vrL2TransactionTotal, Value = GetNull(mdl.vrL2TransactionTotal) },
                new() { NpgsqlDbType = NpgsqlDbType.Numeric, ParameterName = vrL2TransactionItemTotal, Value = GetNull(mdl.vrL2TransactionItemTotal) },

                new() { NpgsqlDbType = NpgsqlDbType.Numeric, ParameterName = vrTotal, Value = GetNull(mdl.vrTotal) },
            });
        }

        public long InsertCompanyFatura(Tb_CompanyFatura mdl, bool retId = false)
        {
            const string query =
                "INSERT INTO \"CompanyFatura\" (\"fkCompany\",\"nuYear\",\"nuMonth\"," +
                "\"vrSubscriptionL1\",\"vrL1Transaction\",\"vrL1TransactionItem\"" +
                ",\"nuQtdL1Trans\",\"nuQtdL1TransItem\",\"vrL1TransactionTotal\",\"vrL1TransactionItemTotal\"," +
                "\"vrSubscriptionL2\",\"vrL2Transaction\",\"vrL2TransactionItem\"" +
                ",\"nuQtdL2Trans\",\"nuQtdL2TransItem\",\"vrL2TransactionTotal\",\"vrL2TransactionItemTotal\"," +
                "" +
                "\"vrTotal\") " +
                "VALUES " +
                "(@fkCompany,@nuYear,@nuMonth," +
                "@vrSubscriptionL1,@vrL1Transaction,@vrL1TransactionItem,@nuQtdL1Trans,@nuQtdL1TransItem,@vrL1TransactionTotal,@vrL1TransactionItemTotal," +
                "@vrSubscriptionL2,@vrL2Transaction,@vrL2TransactionItem,@nuQtdL2Trans,@nuQtdL2TransItem,@vrL2TransactionTotal,@vrL2TransactionItemTotal," +
                "@vrImpostos, @vrTotal);";

            const string currval = "select currval('public.\"CompanyFatura_id_seq\"');";
            using var cmd = new NpgsqlCommand(retId ? query + currval : query, db);
            SetParamsCompanyFatura(cmd, mdl);
            if (retId) return (long)cmd.ExecuteScalar();
            cmd.ExecuteNonQuery();
            return 0;
        }

        public void UpdateCompanyFatura(Tb_CompanyFatura mdl)
        {
            const string query = "update \"CompanyFatura\" set " +
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
                " where id=@id";

            using var cmd = new NpgsqlCommand(query, db);
            SetParamsCompanyFatura(cmd, mdl);
            cmd.ExecuteNonQuery();
        }
    }
}
