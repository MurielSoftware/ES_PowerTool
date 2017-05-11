﻿using Desktop.Shared.Core.Context;
using Desktop.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Core.Services
{
    /// <summary>
    /// The Base service. All services should extend this service.
    /// </summary>
    public abstract class BaseService : IService
    {
        protected IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
