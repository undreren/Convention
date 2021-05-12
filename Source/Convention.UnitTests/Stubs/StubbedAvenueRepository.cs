using Convention.WebApi.Services;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Convention.UnitTests.Stubs
{
    public class StubbedAvenueRepository : IAvenueRepository
    {
        public Func<string, Task<Guid>> CreateAvenueFunc { get; set; }
            = name => throw new AssertionException("CreateAsync was not configured on stub");

        public Task<Guid> CreateAvenue(string name)
        {
            return CreateAvenueFunc(name);
        }
    }
}
