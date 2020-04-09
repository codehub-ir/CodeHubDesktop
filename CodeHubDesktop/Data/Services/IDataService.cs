using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeHubDesktop.Data.Services
{
    public interface IDataService<T>
    {
        Task<IEnumerable<T>> GetAllSnippets();
        Task<IEnumerable<T>> GetSnippet(string filter);
        Task<T> CreateSnippet(T entity);
        Task<T> UpdateSnippet(int id, T entity);
        Task<bool> DeleteSnippet(int id);
    }
}
