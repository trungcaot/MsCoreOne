using MsCoreOne.Application.Common.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MsCoreOne.Application.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
    }
}
