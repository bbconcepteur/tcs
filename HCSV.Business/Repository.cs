using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HCSV.Models;

namespace HCSV.Business
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> GetTable();

        /// <summary>
        /// Get all the element of this repository
        /// </summary>
        /// <returns>
        IList<T> GetAll();

        /// <summary>
        /// Load the entities using a linq expression filter
        /// </summary>
        /// <typeparam name="T">the entity type to load</typeparam>
        /// <param name="where">where condition</param>
        /// <returns>the loaded entity</returns>
        IList<T> GetMany(Expression<Func<T, bool>> whereCondition);

        /// <summary>
        /// Get a selected extiry by the object primary key ID
        /// </summary>
        /// <param name="id">Primary key ID</param>
        T GetByID(object id);

        /// <summary>
        /// Get a selected extiry by using a linq expression filter
        /// </summary>
        /// <typeparam name="T">the entity type to get</typeparam>
        /// <param name="where">where condition</param>
        T GetSingle(Expression<Func<T, bool>> whereCondition);

        /// <summary>
        /// Add entity to the repository
        /// </summary>
        /// <param name="obj">the entity to add</param>
        void Create(T obj);

        /// <summary>
        /// Updates entity within the the repository
        /// </summary>
        /// <param name="obj">the entity to update</param>
        /// <returns>The updates entity</returns>
        void Update(T obj);

        /// <summary>
        /// Mark entity to be deleted within the repository
        /// </summary>
        /// <param name="id"></param>
        void Delete(object id);

        void Save();

        Task SaveAsync();
    }

    public class Repository<T> : IRepository<T> where T : class
    {

        protected TCSEntities db;
        private DbSet<T> table;

        public Repository()
        {
            db = new TCSEntities();
            table = db.Set<T>();
        }

        public Repository(TCSEntities db)
        {
            this.db = db;
            table = db.Set<T>();
        }

        public DbSet<T> GetTable()
        {
            return table;
        }

        protected DbSet<T1> GetTable<T1>()
            where T1 : class
        {
            return db.Set<T1>();
        }

        public virtual void Create(T obj)
        {
            table.Add(obj);
        }

        public virtual void Update(T obj)
        {
            table.Attach(obj);
            db.Entry(obj).State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public virtual IList<T> GetAll()
        {
            return table.ToList();
        }

        public virtual IList<T> GetMany(Expression<Func<T, bool>> whereCondition)
        {
            return table.Where(whereCondition).ToList();
        }

        public virtual T GetByID(object id)
        {
            return table.Find(id);
        }

        public virtual T GetSingle(Expression<Func<T, bool>> whereCondition)
        {
            return table.FirstOrDefault(whereCondition);
        }

        public virtual void Save()
        {
            db.SaveChanges();
        }

        public virtual async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}