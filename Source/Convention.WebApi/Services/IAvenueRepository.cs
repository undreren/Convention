using System;
using System.Threading.Tasks;

namespace Convention.WebApi.Services
{
    public interface IAvenueRepository
    {
        Task<Guid> CreateAvenue(string name);
    }
}
