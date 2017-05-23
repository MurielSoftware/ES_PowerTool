using Desktop.Data.Core.Model;
using Desktop.Shared.Core.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Desktop.Data.Core.DAL
{
    public class GenericRepository : BaseRepository
    {
        public GenericRepository(Connection connection)
            : base(connection)
        {

        }

        /// <summary>
        /// Finds the entity by its ID.
        /// </summary>
        /// <typeparam name="T">The type of the entity</typeparam>
        /// <param name="id">The ID of the entity</param>
        /// <returns>The found entity</returns>
        public virtual T Find<T>(Guid id) where T : BaseEntity
        {
            return GetContext().Set<T>().Find(id);
       //     return GetContext().Set<T>().Where(x => x.Id == id).AsNoTracking().SingleOrDefault();
        }

        /// <summary>
        /// Finds the entity and track it by its ID
        /// </summary>
        /// <typeparam name="T">The type of the entity</typeparam>
        /// <param name="id">The ID of the entity</param>
        /// <returns>The found entity</returns>
        public virtual T FindTracking<T>(Guid id) where T : BaseEntity
        {
            return GetContext().Set<T>().Where(x => x.Id == id).SingleOrDefault();
        }

        /// <summary>
        /// Finds the entity by its prefix
        /// </summary>
        /// <typeparam name="T">The type of the entity</typeparam>
        /// <param name="baseFilterDto">The filtering DTO to make the where part</param>
        /// <param name="selector">The selector for select the attributes</param>
        /// <returns>The list of the referencedDto for the appropriate search DTO</returns>
        //internal List<ReferenceDto> FindByPrefix<T>(BaseFilterDto baseFilterDto, Expression<Func<T, ReferenceDto>> selector) where T : BaseEntity
        //{
        //    return _modelContext.Set<T>()
        //        .Where(ExpressionQueryBuilder.BuildWhere<T>(baseFilterDto))
        //        .Select(selector)
        //        .ToList();
        //}

        /// <summary>
        /// Checks if something exists.
        /// </summary>
        /// <typeparam name="T">The type of the entity</typeparam>
        /// <param name="where">The where statement</param>
        /// <returns>True if it exists, otherwise it returns false</returns>
        public virtual bool Exists<T>(Expression<Func<T, bool>> where) where T : BaseEntity
        {
            return GetContext().Set<T>().Where(where).Count() > 0;
        }

        public virtual List<Guid> FindIds<T>(Expression<Func<T, bool>> where) where T : BaseEntity
        {
            return GetContext().Set<T>().Where(where).Select(x => x.Id).ToList();
        }

        /// <summary>
        /// Counts the records in the table.
        /// </summary>
        /// <typeparam name="T">The type of the entity</typeparam>
        /// <returns>The count of the records</returns>
        public virtual int Count<T>() where T : BaseEntity
        {
            return GetContext().Set<T>().Count();
        }

        /// <summary>
        /// Persists the entity.
        /// </summary>
        /// <typeparam name="T">The type of the entity</typeparam>
        /// <param name="entity">The entity to persist</param>
        /// <returns>The persisted entity</returns>
        public virtual T Persist<T>(T entity) where T : BaseEntity
        {
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
                entity.LastUpdate = DateTime.Now;
                entity = GetContext().Set<T>().Add(entity);
            }
            else
            {
                entity.LastUpdate = DateTime.Now;
                GetContext().Entry(entity).State = EntityState.Modified;
            }
            Flush();
            return entity;
        }

        public virtual void PersistAsNews<T>(List<T> entities) where T : BaseEntity
        {
            foreach(T entity in entities)
            {
                entity.LastUpdate = DateTime.Now;
                GetContext().Set<T>().Add(entity);
            }
            Flush();
        }

        /// <summary>
        /// Delets the entity
        /// </summary>
        /// <typeparam name="T">The type of the entity</typeparam>
        /// <param name="entity">The entity to delete</param>
        public virtual void Delete<T>(T entity) where T : BaseEntity
        {
            GetContext().Set<T>().Remove(entity);
            Flush();
        }

        public virtual void DeleteRange<T>(Func<T, bool> where) where T : BaseEntity
        {
            GetContext().Set<T>().RemoveRange(GetContext().Set<T>().Where(where));
            Flush();
        }

        /// <summary>
        /// Flushes the changes to the database.
        /// </summary>
        public virtual void Flush()
        {
            GetContext().SaveChanges();
        }

        /// <summary>
        /// Disposes the database.
        /// </summary>
        public void Dispose()
        {
            GetContext().Dispose();
        }

        /// <summary>
        /// Attaches the entity to the context.
        /// </summary>
        /// <typeparam name="T">The type of the entity</typeparam>
        /// <param name="entity">The entity to be attached</param>
        public void Attach<T>(T entity) where T : BaseEntity
        {
            GetContext().Set<T>().Attach(entity);
        }

        /// <summary>
        /// Attaches the entity to the context.
        /// </summary>
        /// <param name="type">The type of the entity</param>
        /// <param name="entity">The entity to be attached</param>
        public void Attach(Type type, BaseEntity entity)
        {
            GetContext().Set(type).Attach(entity);
        }

        /// <summary>
        /// Executes the sql script.
        /// </summary>
        /// <param name="script">The scrpt to be executed</param>
        public void ExecuteNonQuery(string script)
        {
            GetContext().Database.ExecuteSqlCommand(script);
        }
    }
}
