using Desktop.Data.Core.Converters;
using Desktop.Data.Core.DAL;
using Desktop.Data.Core.Model;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.Dtos;
using Desktop.Shared.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Data.Core.BAL
{
    public class GenericCRUDService<T, U> : BaseService, ICRUDService<T>
        where T : BaseDto
        where U : BaseEntity
    {
        protected GenericRepository _genericRepository;

        protected BaseConvertProvider<T, U> _dtoToEntityConverter = new DtoToEntityConvertProvider<T, U>();
        protected BaseConvertProvider<U, T> _entityToDtoConverter = new EntityToDtoConvertProvider<U, T>();

        public GenericCRUDService(Connection connection)
            : base(connection)
        {
            _genericRepository = new GenericRepository(connection);
        }

        public virtual void Delete(Guid id)
        {
            U entityToDelete = _genericRepository.FindTracking<U>(id);
            ValidationBeforeDelete(entityToDelete);
            DoDelete(entityToDelete);
        }

        public virtual T Persist(T dto)
        {
            ValidationBeforePersist(dto);
            U persistedEntity = DoPersist(CreateEntity(dto));
            return CreateDto(persistedEntity);
        }

        public virtual T Read(Guid id)
        {
            return CreateDto(_genericRepository.FindNoTracking<U>(id));
        }

        public void Dispose()
        {
            _genericRepository.Dispose();
        }

        /// <summary>
        /// Does the persist to the database.
        /// </summary>
        /// <param name="entity">The entity to persist</param>
        /// <returns>Persisted entity</returns>
        protected virtual U DoPersist(U entity)
        {
            return _genericRepository.Persist<U>(entity);
        }

        /// <summary>
        /// Does the delete the entity from the database.
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        protected virtual void DoDelete(U entity)
        {
            _genericRepository.Delete<U>(entity);
        }

        /// <summary>
        /// Validation before persist. It raises the exception if the persist is not allowed.
        /// </summary>
        /// <param name="entity">The entity to check</param>
        protected virtual void ValidationBeforePersist(T dto)
        {

        }

        /// <summary>
        /// Validation before delete. It raises the exception if the delete is not allowed.
        /// </summary>
        /// <param name="entity">The entity to check</param>
        protected virtual void ValidationBeforeDelete(U entity)
        {

        }

        /// <summary>
        /// Creates the entity. Should provide some prepersist operations.
        /// </summary>
        /// <param name="entity">The entity to prepare to persist.</param>
        protected virtual U CreateEntity(T dto)
        {
            return _dtoToEntityConverter.Convert(_connection, dto);
        }

        /// <summary>
        /// Create the entity after read.
        /// </summary>
        /// <param name="entity">The read entity</param>
        protected virtual T CreateDto(U entity)
        {
            return _entityToDtoConverter.Convert(_connection, entity);
        }

        /// <summary>
        /// Checks if the entity exists.
        /// </summary>
        /// <param name="entity">The entity to check</param>
        /// <returns>Return <code>true</code> if the entity exists</returns>
        //protected bool EntityExists(T dto)
        //{
        //    return dto.Id != Guid.Empty;
        //}

        protected bool EntityExists(U entity)
        {
            return entity.Id != Guid.Empty;
        } 
    }
}
