﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplePersistence.Example.WebApi.UoW.Area;
using SimplePersistence.Example.WebApi.UoW.EF.Area;
using SimplePersistence.UoW.EF;

namespace SimplePersistence.Example.WebApi.UoW.EF
{
    public class ApiUnitOfWork : EFUnitOfWork, IApiUnitOfWork
    {
        private readonly Lazy<ILoggingWorkArea> _lazyLoggingWorkArea;

        public ApiUnitOfWork(DbContext context) : base(context)
        {
            _lazyLoggingWorkArea = new Lazy<ILoggingWorkArea>(() => new LoggingWorkArea(context));
        }

        public ILoggingWorkArea Logging { get { return _lazyLoggingWorkArea.Value; } }
    }
}