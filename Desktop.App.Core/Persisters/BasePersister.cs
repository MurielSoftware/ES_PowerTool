using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.Dtos;
using Desktop.Shared.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.App.Core.Persisters
{
    public class BasePersister<T> where T : BaseDto
    {
        private ICRUDService<T> _service;
        private T _dto;

        public BasePersister(ICRUDService<T> service, T dto)
        {
            _service = service;
            _dto = dto;
        }

        public virtual void Persist()
        {
            BeforePersist();
            _dto = _service.Persist(_dto);
            AfterPersist();
        }

        protected virtual void BeforePersist()
        {

        }

        protected virtual void AfterPersist()
        {

        }

        protected T GetDto()
        {
            return _dto;
        }

        protected ICRUDService<T> GetService()
        {
            return _service;
        }
    }
}
