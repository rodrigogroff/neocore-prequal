using System;

namespace Master.Controller.Infra.Manager
{
    public class ServiceManager : IDisposable
    {
        private readonly MasterController MyController;
        private bool disposed = false;

        public ServiceManager(MasterController myController)
        {
            this.MyController = myController;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    MyController.FinishService();
                }
                disposed = true;
            }
        }

        ~ServiceManager()
        {
            Dispose(false);
        }
    }
}
