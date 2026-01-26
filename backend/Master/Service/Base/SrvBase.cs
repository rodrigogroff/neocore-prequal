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
using Master.Service.Base.Infra.Helper;

namespace Master.Service.Base
{
    public class SrvBase
    {
        public List<BaseRepository> currentAllocRepos = null;
        public List<SrvBase> currentAllocServices = null;

        public ICompanyRepository iRepoCompany = null;
        public IUserRepository iRepoUser = null;
        public IBureauRepository iRepoBureau = null;
        public IPrequalRepository iRepoPrequal = null;

        public NpgsqlConnection MainDb = null;
        public LocalNetwork Network = null;

        private HelperCheck? _helperCheck;
        private HelperMisc? _helperMisc;
        private HelperFileManager? _helperFileManager;

        public HelperCheck HelperCheck() => _helperCheck ??= new();
        public HelperMisc HelperMisc() => _helperMisc ??= new();
        public HelperFileManager HelperFileManager() => _helperFileManager ??= new();

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

        protected IPrequalRepository RepoPrequal(bool bCache = false)
        {
            if (iRepoPrequal != null)
                return iRepoPrequal;
            return CreateAndTrackRepo<PrequalRepository>(enableCache: bCache);
        }

        protected IUserRepository RepoUser(bool bCache = false)
        {
            if (iRepoUser != null)
                return iRepoUser;
            return CreateAndTrackRepo<UserRepository>(enableCache: bCache);
        }

        public ICompanyRepository RepoCompany(bool bCache = false)
        {
            if (iRepoCompany != null)
                return iRepoCompany;
            return CreateAndTrackRepo<CompanyRepository>(enableCache: bCache);
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

            MainDb = new NpgsqlConnection(network.database);
            MainDb.Open();
            return MainDb;
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
            iRepoPrequal = null;
            iRepoCompany = null;
            iRepoUser = null;
            iRepoBureau = null;

            // --------------------------
            // helpers
            // --------------------------

            _helperCheck = null;
            _helperMisc = null;
            _helperFileManager = null;

            // --------------------------
            // envs
            // --------------------------

            if (currentAllocServices is not null)
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
