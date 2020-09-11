using System;
using System.Collections.Generic;
using System.Text;

namespace MsCoreOne.Application.Common.Interfaces
{
    public interface IMemoryCacheManager
    {
        bool TryGetValue<TItem>(string key, out TItem value);

        TItem Set<TItem>(string key, TItem value);
    }
}
