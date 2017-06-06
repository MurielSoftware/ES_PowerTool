using Desktop.Shared.Core.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Data.Core.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        private ModelContext _modelContext;
        private DbContextTransaction _transaction;

        public UnitOfWork(ModelContext modelContext)
        {
            _modelContext = modelContext;
            //_modelContext = new ModelContext(connectionString.GetConnectionString());
        }

        public IDatabaseContext GetContext()
        {
            return _modelContext;
        }

        public void StartTransaction()
        {
            if (_transaction == null)
            {
                if (_modelContext.Database.Connection.State != System.Data.ConnectionState.Open)
                {
                    _modelContext.Database.Connection.Open();
                }
                _transaction = _modelContext.Database.BeginTransaction();
            }
        }

        public void EndTransaction()
        {
            if (_transaction == null)
            {
                return;
            }

            try
            {
                Commit();
            }
            catch (Exception ex)
            {
                Rollback();
            }
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }
            _modelContext.Dispose();
        }

        private void Commit()
        {
            //_modelContext.SaveChanges();
            _transaction.Commit();
            _transaction = null;
        }

        private void Rollback()
        {
            _transaction.Rollback();
            _transaction = null;
        }
    }
}
