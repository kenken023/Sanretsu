using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sanretsu.Services
{
    public interface IChildDataStore<T>
    {
        Task<IEnumerable<T>> GetChildItemsAsync(int parentId);
    }
}
