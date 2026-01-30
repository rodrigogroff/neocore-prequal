using Master.Entity;
using Npgsql;
using System.Runtime;
using System;
using System.Collections.Generic;
using Master.Repository;
using Master.Repository.Domain.Company;
using Master.Repository.Domain.User;
using Master.Repository.Domain.Prequal;
using Master.Repository.Domain.Bureau;
using Master.Repository.Domain.Infra;
using Master.Entity.Dto.Infra;

namespace Master.Service.Base
{
    public class SrvBase
    {
        public 
            const 
                string 
                    TOKEN_SRV_NDISP = "Este endpoint não está disponível",
                    TOKEN_SRV_NDISP_ALT = "Este endpoint não está disponível no momento.";

        public NpgsqlConnection MainDb;
        public LocalNetwork Network;

        public IFeatureRepository iRepoFeature;
        public ILogRepository iRepoLog;

        public ICompanyRepository iRepoCompany;
        public IUserRepository iRepoUser;
        public IBureauRepository iRepoBureau;
        public IPrequalRepository iRepoPrequal;

        public List<BaseRepository> currentAllocRepos;
        public List<SrvBase> currentAllocServices;

        public string endpoint = string.Empty;
        public string errorCode = string.Empty;
        public string errorMessage = string.Empty;

        private T CreateAndTrackRepo<T>(bool enableCache = false) where T : BaseRepository, new()
        {
            currentAllocRepos ??= [];
            var repo = new T { db = MainDb };
            if (enableCache) repo.EnableCache();
            currentAllocRepos.Add(repo);
            return repo;
        }

        protected IFeatureRepository RepoFeature()
        {
            if (iRepoFeature != null)
                return iRepoFeature;
            return CreateAndTrackRepo<FeatureRepository>();
        }

        protected ILogRepository RepoLog()
        {
            if (iRepoLog != null)
                return iRepoLog;
            return CreateAndTrackRepo<LogRepository>();
        }
        
        public ICompanyRepository RepoCompany(bool bCache = false)
        {
            if (iRepoCompany != null)
                return iRepoCompany;
            return CreateAndTrackRepo<CompanyRepository>(enableCache: bCache);
        }

        protected IUserRepository RepoUser(bool bCache = false)
        {
            if (iRepoUser != null)
                return iRepoUser;
            return CreateAndTrackRepo<UserRepository>(enableCache: bCache);
        }

        protected IPrequalRepository RepoPrequal(bool bCache = false)
        {
            if (iRepoPrequal != null)
                return iRepoPrequal;
            return CreateAndTrackRepo<PrequalRepository>(enableCache: bCache);
        }


        public IBureauRepository RepoBureau(bool bCache = false)
        {
            if (iRepoBureau != null)
                return iRepoBureau;
            return CreateAndTrackRepo<BureauRepository>(enableCache: bCache);
        }

        public SrvBase RegisterService(SrvBase service)
        {
            if (currentAllocServices == null)   
                currentAllocServices = [];

            service.Network = Network;

            currentAllocServices.Add(service);

            return GetService(currentAllocServices.Count - 1);
        }

        SrvBase GetService(int index)
        {
            return currentAllocServices[index];
        }

        public NpgsqlConnection StartDatabase(LocalNetwork network)
        {
            if (network == null)
                return null;

            this.MainDb = new NpgsqlConnection(network.database);
            this.MainDb.Open();

            if (!string.IsNullOrEmpty(this.endpoint))
            {
                var repo = RepoFeature();

                var feature = repo.GetFeature(this.endpoint);

                if (feature == null)
                    throw new Exception(TOKEN_SRV_NDISP);

                if (feature.bActive != true)
                    throw new Exception(TOKEN_SRV_NDISP_ALT);
            }

            return MainDb;
        }

        public bool LogException(Exception ex, DtoAuthenticatedUser user = null, int? fkCompany = null)
        {
            this.errorCode = "FAIL";
            this.errorMessage = ex.ToString();

            var repoLog = this.RepoLog();

            repoLog.InsertLogApplication(new Entity.Database.Domain.Infra.Tb_LogApplication
            {
                dtLog = DateTime.Now,
                fkCompany = user != null ? (int)user.fkCompany : fkCompany != null ? (int)fkCompany : 0,
                fkUser = user != null ? (int)user.fkUser : 0,
                stEndpoint = this.endpoint,
                stExceptionData = ex.Message,
            });

            return false;
        }

        public void DisposeService()
        {
            // --------------------------
            // repositories
            // --------------------------

            if (currentAllocRepos?.Count > 0)
            {
                foreach (var repo in currentAllocRepos)
                    repo?.DisposeRepository();

                currentAllocRepos.Clear();
                currentAllocRepos = null;
            }

            iRepoFeature = null;
            iRepoLog = null;
            iRepoPrequal = null;
            iRepoCompany = null;
            iRepoUser = null;
            iRepoBureau = null;

            // --------------------------
            // envs
            // --------------------------

            if (currentAllocServices?.Count > 0)
            {
                foreach (var item in currentAllocServices)
                    item.DisposeService();
                currentAllocServices.Clear();
                currentAllocServices = null;
            }

            // --------------------------
            // database
            // --------------------------

            if (MainDb != null)
            {
                if (MainDb.State == System.Data.ConnectionState.Open)
                    MainDb.Close();
                MainDb = null;
            }

            GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect();
        }
    }
}
