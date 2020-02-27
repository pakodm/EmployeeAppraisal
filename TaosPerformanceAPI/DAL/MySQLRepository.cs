using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using TaosPerformanceAPI.Models;

namespace TaosPerformanceAPI.DAL
{
    public class MySQLRepository
    {
        protected DbContext _context;

        public MySQLRepository(MySQLContext context)
        {
            _context = context;
        }

        public virtual IEnumerable<T> GetAll<T>(Expression<Func<T, T>> select = null) where T : class, new()
        {
            IQueryable<T> query = _context.Set<T>();
            if (select != null)
            {
                query = query.Select(select);
            }
            return query.AsEnumerable();
        }

        public virtual int Count<T>() where T : class, new()
        {
            return _context.Set<T>().Count();
        }

        public virtual int CountWhere<T>(List<Expression<Func<T, bool>>> whereExpressions) where T : class, new()
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var item in whereExpressions)
            {
                query = query.Where(item);
            }
            return query.Count();
        }

        public virtual IEnumerable<T> AllIncluding<T>(params Expression<Func<T, object>>[] includeProperties) where T : class, new()
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.AsEnumerable();
        }

        public virtual IEnumerable<T> GetAllWhere<T>(List<Expression<Func<T, bool>>> whereExpressions, bool asNoTracking = false, params Expression<Func<T, object>>[] includeProperties) where T : class, new()
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            foreach (var item in whereExpressions)
            {
                query = query.Where(item);
            }

            return asNoTracking ? query.AsNoTracking().AsEnumerable() : query.AsEnumerable();
        }

        public T GetSingle<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public T GetSingle<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties) where T : class, new()
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate).FirstOrDefault();
        }

        public virtual IEnumerable<T> FindBy<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            return _context.Set<T>().Where(predicate);
        }

        public virtual void Add<T>(T entity) where T : class, new()
        {
            EntityEntry dnEntityEntry = _context.Entry(entity);
            _context.Set<T>().Add(entity);
        }

        public virtual void Add<T>(object entityObject) where T : class, new()
        {
            T entity = (T)entityObject;
            EntityEntry dbEntityEntry = _context.Entry(entity);
            _context.Set<T>().Add(entity);
        }

        public virtual void AddRange(IEnumerable<object> entityList)
        {
            _context.AddRange(entityList);
        }

        public virtual void Update<T>(T entity) where T : class, new()
        {
            EntityEntry dbEntityEntry = _context.Entry(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete<T>(T entity) where T : class, new()
        {
            EntityEntry dbEntityEntry = _context.Entry(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public virtual void DeleteWhere<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            IEnumerable<T> entities = _context.Set<T>().Where(predicate);

            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Deleted;
            }
        }

        public virtual void Commit()
        {
            _context.SaveChanges();
        }
    }

    public class TaosPerformanceDB : MySQLRepository
    {
        public TaosPerformanceDB(MySQLContext context) : base(context) { }

        public Usuarios GetUser(string login, string pwd)
        {
            var user = _context.Set<Usuarios>().Where(u => u.Id.Equals(login));
            return user.FirstOrDefault();
        }

        public List<Empleados> GetEmployeesToEvaluate(string leaderId, int companyId)
        {
            List<Empleados> aEvaluar = new List<Empleados>();

            var listaEvaluados = GetAllWhere(new List<Expression<Func<Relaciones, bool>>> { a => a.IdLider.Equals(leaderId) && a.IdEmpresa == companyId }, true)
                .OrderBy(a => a.IdEmpleado)
                .Select(x => x.IdEmpleado);
            // string ids = string.Join(",", listaEvaluados.Select(x => x.IdEmpleado));

            aEvaluar = _context.Set<Empleados>().Where(a => listaEvaluados.Contains(a.Id)).AsNoTracking().ToList();

            return aEvaluar;
        }
    }
}
