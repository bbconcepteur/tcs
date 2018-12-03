using System;
using System.Collections;
using System.Threading.Tasks;
using HCSV.Business.Business;
using HCSV.Models;

namespace HCSV.Business
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveAsync();
        int Save();

        ILanguageBusiness LanguageBusiness { get; }
        INewsBusiness NewsBusiness { get; }
        IAccountBusiness AccountBusiness { get; }

        IDashbroadBusiness DashbroadBussiness { get; }
        IMenuBusiness MenuBusiness { get; }
        IContactBusiness ContactBusiness { get; }
        IFeedbackBusiness FeedbackBusiness { get; }
    }

    public class UnitOfWork : IUnitOfWork
    {
        private TCSEntities _context;
        private Hashtable _repositories;
        private bool _disposed = false;
        public UnitOfWork()
        {
            _context = new TCSEntities();
        }

        public UnitOfWork(TCSEntities context)
        {
            _context = context;
        }

        public Task<int> SaveAsync()
        {
            // Save changes with the default options
            return _context.SaveChangesAsync();
        }

        public int Save()
        {
            // Save changes with the default options
            return _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType
                            .MakeGenericType(typeof(T)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)_repositories[type];
        }


        #region Initization

        public ILanguageBusiness LanguageBusiness
        {
            get { return new LanguageBusiness(_context); }
        }

        public INewsBusiness NewsBusiness
        {
            get { return new NewsBusiness(_context); }
        }
        public IAccountBusiness AccountBusiness
        {
            get { return new AccountBusiness(_context); }
        }

        public IDashbroadBusiness DashbroadBussiness
        {
            get { return new DashbroadBusiness(_context); }
        }

        public IMenuBusiness MenuBusiness
        {
            get { return new MenuBusiness(_context); }
        }
        public IContactBusiness ContactBusiness
        {
            get { return new ContactBusiness(_context); }
        }

        public IFeedbackBusiness FeedbackBusiness { get { return new FeedbackBusiness(_context); } }
        #endregion
    }
}
